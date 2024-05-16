using Business.DTO;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class SettingsService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISettingsRepository _settingsRepository;

    public SettingsService(UserManager<ApplicationUser> userManager, ISettingsRepository userSettingsRepository)
    {
        _userManager = userManager;
        _settingsRepository = userSettingsRepository;
    }

    public async Task<SettingsDto> GetUserSettingsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var userSetting = await _settingsRepository.GetUserSettingsAsync(userId);

        if (user == null || userSetting == null)
        {
            return null;
        }

        return new SettingsDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ExpenseLimit = userSetting.ExpenseLimit ?? 0,
            IncomeLimit = userSetting.IncomeLimit ?? 0
        };
    }

    public async Task UpdateSettingsAsync(SettingsDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);

        if (user != null)
        {
            if (user.UserName != dto.UserName)
            {
                user.UserName = dto.UserName;
            }
            if (user.Email != dto.Email)
            {
                user.Email = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.Password) && dto.Password == dto.ConfirmPassword)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
            }

            await _userManager.UpdateAsync(user);
        }

        var userSetting = await _settingsRepository.GetUserSettingsAsync(dto.UserId);
        if (userSetting != null)
        {
            if (userSetting.ExpenseLimit != dto.ExpenseLimit)
            {
                userSetting.ExpenseLimit = dto.ExpenseLimit;
            }
            if (userSetting.IncomeLimit != dto.IncomeLimit)
            {
                userSetting.IncomeLimit = dto.IncomeLimit;
            }

            await _settingsRepository.UpdateUserSettingsAsync(userSetting);
        }
    }
}
