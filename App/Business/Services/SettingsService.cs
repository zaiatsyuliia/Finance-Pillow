using Business.DTO;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class SettingsService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISettingsRepository _userSettingsRepository;

    public SettingsService(UserManager<ApplicationUser> userManager, ISettingsRepository userSettingsRepository)
    {
        _userManager = userManager;
        _userSettingsRepository = userSettingsRepository;
    }
    public async Task<SettingsDto> GetUserSettingsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found.");

        var userSettings = await _userSettingsRepository.GetUserSettingsAsync(userId);
        if (userSettings == null) throw new Exception("User settings not found.");

        return new SettingsDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ExpenseLimit = userSettings.ExpenseLimit ?? 0,
            IncomeLimit = userSettings.IncomeLimit ?? 0
        };
    }

    public async Task UpdateSettingsAsync(SettingsDto dto)
    {
        Console.WriteLine("Settings Post Service!");

        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (!string.IsNullOrWhiteSpace(dto.UserName))
        {
            user.UserName = dto.UserName;
            Console.WriteLine("Username Changed!");
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            user.Email = dto.Email;
            Console.WriteLine("Email Changed!");
        }

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            throw new Exception("Failed to update user identity details.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("The new password and confirmation password do not match.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, dto.Password);
            if (!passwordResult.Succeeded)
            {
                throw new Exception("Failed to update user password.");
            }
            Console.WriteLine("Password Changed!");
        }

        if (dto.ExpenseLimit >= 0)
        {
            await _userSettingsRepository.UpdateExpenseLimitAsync(dto.UserId, dto.ExpenseLimit);
        }
        if (dto.IncomeLimit >= 0)
        {
            await _userSettingsRepository.UpdateExpenseLimitAsync(dto.UserId, dto.ExpenseLimit);
        }

        Console.WriteLine("Settings Post Service End!");
    }
}
