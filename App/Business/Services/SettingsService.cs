using Business.DTO;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        if (user.UserName != dto.UserName)
        {
            user.UserName = dto.UserName;
        }

        if (user.Email != dto.Email)
        {
            var emailChangeResult = await _userManager.SetEmailAsync(user, dto.Email);
            if (!emailChangeResult.Succeeded)
            {
                throw new Exception("Failed to update email.");
            }
        }

        if (!string.IsNullOrEmpty(dto.Password) && dto.Password == dto.ConfirmPassword)
        {
            var passwordChangeResult = await _userManager.RemovePasswordAsync(user);
            if (passwordChangeResult.Succeeded)
            {
                passwordChangeResult = await _userManager.AddPasswordAsync(user, dto.Password);
            }
            if (!passwordChangeResult.Succeeded)
            {
                throw new Exception("Failed to update password.");
            }
        }

        if (user.ExpenseLimit != dto.ExpenseLimit)
        {
            user.ExpenseLimit = dto.ExpenseLimit;
        }

        if (user.IncomeLimit != dto.IncomeLimit)
        {
            user.IncomeLimit = dto.IncomeLimit;
        }

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new Exception("Failed to update user settings.");
        }
    }
}
