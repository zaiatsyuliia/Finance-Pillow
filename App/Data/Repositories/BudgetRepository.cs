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

    Task<List<History>> GetUserHistoryTimeAsync(string userId, DateTime startDate, DateTime endDate);

    Task<List<History>> GetUserExpenseHistoryAsync(string userId);

    Task<List<History>> GetUserIncomeHistoryAsync(string userId);
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

    public async Task<List<History>> GetUserHistoryTimeAsync(string userId, DateTime startDate, DateTime endDate)
    {
        endDate = endDate.Date.AddDays(1).AddSeconds(-1);

        var userHistory = await _context.Histories
            .Where(h => h.UserId == userId && h.Time >= startDate && h.Time <= endDate)
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userHistory;
    }

    public async Task<List<History>> GetUserExpenseHistoryAsync(string userId)
    {
        var userExpenseHistory = await _context.Histories
            .Where(h => h.UserId == userId && h.TransactionType == "expense")
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userExpenseHistory;
    }

    public async Task<List<History>> GetUserIncomeHistoryAsync(string userId)
    {
        var userIncomeHistory = await _context.Histories
            .Where(h => h.UserId == userId && h.TransactionType == "income")
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userIncomeHistory;
    }
}
