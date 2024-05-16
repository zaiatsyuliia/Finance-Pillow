using Business.DTO;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class SettingsService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SettingsService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<SettingsDto> GetUserSettingsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return null; // або обробка помилки
        }

        return new SettingsDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ExpenseLimit = user.ExpenseLimit,
            IncomeLimit = user.IncomeLimit
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

            if (user.ExpenseLimit != dto.ExpenseLimit)
            {
                user.ExpenseLimit = dto.ExpenseLimit;
            }
            if (user.IncomeLimit != dto.IncomeLimit)
            {
                user.IncomeLimit = dto.IncomeLimit;
            }

            await _userManager.UpdateAsync(user);
        }
    }
}
