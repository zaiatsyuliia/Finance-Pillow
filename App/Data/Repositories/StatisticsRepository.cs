using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IStatisticsRepository
{
    Task<List<ExpenseMonthDaily>> GetExpenseMonthDailyAsync(string userId);

    Task<List<ExpenseMonthTotal>> GetExpenseMonthTotalAsync(string userId);

    Task<List<Expense6MonthsMonthly>> GetExpense6MonthsMonthlyAsync(string userId);

    Task<List<Expense6MonthsTotal>> GetExpense6MonthsTotalAsync(string userId);

    Task<List<ExpenseYearMonthly>> GetExpenseYearMonthlyAsync(string userId);

    Task<List<ExpenseYearTotal>> GetExpenseYearTotalAsync(string userId);

    Task<List<IncomeMonthDaily>> GetIncomeMonthDailyAsync(string userId);

    Task<List<IncomeMonthTotal>> GetIncomeMonthTotalAsync(string userId);

    Task<List<Income6MonthsMonthly>> GetIncome6MonthsMonthlyAsync(string userId);

    Task<List<Income6MonthsTotal>> GetIncome6MonthsTotalAsync(string userId);

    Task<List<IncomeYearMonthly>> GetIncomeYearMonthlyAsync(string userId);

    Task<List<IncomeYearTotal>> GetIncomeYearTotalAsync(string userId);
}

public class StatisticsRepository : IStatisticsRepository
{
    private readonly FPDbContext _context;

    public StatisticsRepository(FPDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExpenseMonthDaily>> GetExpenseMonthDailyAsync(string userId)
    {
        return await _context.ExpenseMonthDailies
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseMonthTotal>> GetExpenseMonthTotalAsync(string userId)
    {
        return await _context.ExpenseMonthTotals
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Expense6MonthsMonthly>> GetExpense6MonthsMonthlyAsync(string userId)
    {
        return await _context.Expense6MonthsMonthlies
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Expense6MonthsTotal>> GetExpense6MonthsTotalAsync(string userId)
    {
        return await _context.Expense6MonthsTotals
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseYearMonthly>> GetExpenseYearMonthlyAsync(string userId)
    {
        return await _context.ExpenseYearMonthlies
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseYearTotal>> GetExpenseYearTotalAsync(string userId)
    {
        return await _context.ExpenseYearTotals
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeMonthDaily>> GetIncomeMonthDailyAsync(string userId)
    {
        return await _context.IncomeMonthDailies
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeMonthTotal>> GetIncomeMonthTotalAsync(string userId)
    {
        return await _context.IncomeMonthTotals
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Income6MonthsMonthly>> GetIncome6MonthsMonthlyAsync(string userId)
    {
        return await _context.Income6MonthsMonthlies
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Income6MonthsTotal>> GetIncome6MonthsTotalAsync(string userId)
    {
        return await _context.Income6MonthsTotals
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeYearMonthly>> GetIncomeYearMonthlyAsync(string userId)
    {
        return await _context.IncomeYearMonthlies
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeYearTotal>> GetIncomeYearTotalAsync(string userId)
    {
        return await _context.IncomeYearTotals
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }
}
