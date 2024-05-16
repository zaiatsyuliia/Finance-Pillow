using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ISettingsRepository
{
    Task<UserSetting> GetUserSettingsAsync(string userId);

    Task UpdateUserSettingsAsync(UserSetting userSetting);
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
            .Include(us => us.User)
            .FirstOrDefaultAsync(us => us.UserId == userId);
    }

    public async Task UpdateUserSettingsAsync(UserSetting userSetting)
    {
        _context.UserSettings.Update(userSetting);
        await _context.SaveChangesAsync();
    }
}
