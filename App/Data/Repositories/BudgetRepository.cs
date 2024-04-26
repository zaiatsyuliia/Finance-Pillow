using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IBudgetRepository
{
    Task<double?> GetUserBudgetAsync(string userId);

    Task<Limit> GetLimitsAsync(string userId);

    Task<List<History>> GetUserHistoryAsync(string userId);
}

public class BudgetRepository : IBudgetRepository
{
    private readonly FPDbContext _context;

    public BudgetRepository(FPDbContext context)
    {
        _context = context;
    }

    public async Task<double?> GetUserBudgetAsync(string userId)
    {
        var userBudget = await _context.Budgets
                    .Where(u => u.UserId == userId)
                    .Select(u => u.Budget1)
                    .FirstOrDefaultAsync();

        return userBudget;
    }

    public async Task<Limit> GetLimitsAsync(string userId)
    {
        return await _context.Limits
                     .FirstOrDefaultAsync(l => l.UserId == userId);
    }

    public async Task<List<History>> GetUserHistoryAsync(string userId)
    {
        var userHistory = await _context.Histories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userHistory;
    }
}
