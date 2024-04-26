using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ISettingsRepository
{
    Task<UserSetting> GetUserSettingsAsync(string userId);

    Task UpdateIncomeLimitAsync(string userId, int newIncomeLimit);

    Task UpdateExpenseLimitAsync(string userId, int newExpenseLimit);
}

public class SettingsRepository : ISettingsRepository
{
    private readonly FPDbContext _context;

    public SettingsRepository(FPDbContext context)
    {
        _context = context;
    }

    public async Task<UserSetting> GetUserSettingsAsync(string userId)
    {
        return await _context.UserSettings
            .FirstOrDefaultAsync(us => us.UserId == userId);
    }

    public async Task UpdateIncomeLimitAsync(string userId, int newIncomeLimit)
    {
        var userSettings = await GetUserSettingsAsync(userId);

        if (userSettings != null)
        {
            userSettings.IncomeLimit = newIncomeLimit;
            await _context.SaveChangesAsync();
            Console.WriteLine("!@#");
        }
    }

    public async Task UpdateExpenseLimitAsync(string userId, int newExpenseLimit)
    {
        var userSettings = await GetUserSettingsAsync(userId);
        if (userSettings != null)
        {
            userSettings.ExpenseLimit = newExpenseLimit;
            await _context.SaveChangesAsync();
            Console.WriteLine("!@#");
        }
    }
}
