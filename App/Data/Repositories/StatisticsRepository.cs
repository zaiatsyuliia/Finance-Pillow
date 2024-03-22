using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IStatisticsRepository
{
    Task<List<ExpenseMonthDaily>> GetExpenseMonthDailyAsync(int userId);

    Task<List<ExpenseMonthTotal>> GetExpenseMonthTotalAsync(int userId);

    Task<List<Expense6MonthsMonthly>> GetExpense6MonthsMonthlyAsync(int userId);

    Task<List<Expense6MonthsTotal>> GetExpense6MonthsTotalAsync(int userId);

    Task<List<ExpenseYearMonthly>> GetExpenseYearMonthlyAsync(int userId);

    Task<List<ExpenseYearTotal>> GetExpenseYearTotalAsync(int userId);

    Task<List<IncomeMonthDaily>> GetIncomeMonthDailyAsync(int userId);

    Task<List<IncomeMonthTotal>> GetIncomeMonthTotalAsync(int userId);

    Task<List<Income6MonthsMonthly>> GetIncome6MonthsMonthlyAsync(int userId);

    Task<List<Income6MonthsTotal>> GetIncome6MonthsTotalAsync(int userId);

    Task<List<IncomeYearMonthly>> GetIncomeYearMonthlyAsync(int userId);

    Task<List<IncomeYearTotal>> GetIncomeYearTotalAsync(int userId);
}

public class StatisticsRepository : IStatisticsRepository
{
    private readonly Context _context;

    public StatisticsRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<ExpenseMonthDaily>> GetExpenseMonthDailyAsync(int userId)
    {
        return await _context.ExpenseMonthDailies
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseMonthTotal>> GetExpenseMonthTotalAsync(int userId)
    {
        return await _context.ExpenseMonthTotals
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<Expense6MonthsMonthly>> GetExpense6MonthsMonthlyAsync(int userId)
    {
        return await _context.Expense6MonthsMonthlies
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<Expense6MonthsTotal>> GetExpense6MonthsTotalAsync(int userId)
    {
        return await _context.Expense6MonthsTotals
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseYearMonthly>> GetExpenseYearMonthlyAsync(int userId)
    {
        return await _context.ExpenseYearMonthlies
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<ExpenseYearTotal>> GetExpenseYearTotalAsync(int userId)
    {
        return await _context.ExpenseYearTotals
            .Where(e => e.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeMonthDaily>> GetIncomeMonthDailyAsync(int userId)
    {
        return await _context.IncomeMonthDailies
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeMonthTotal>> GetIncomeMonthTotalAsync(int userId)
    {
        return await _context.IncomeMonthTotals
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<Income6MonthsMonthly>> GetIncome6MonthsMonthlyAsync(int userId)
    {
        return await _context.Income6MonthsMonthlies
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<Income6MonthsTotal>> GetIncome6MonthsTotalAsync(int userId)
    {
        return await _context.Income6MonthsTotals
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeYearMonthly>> GetIncomeYearMonthlyAsync(int userId)
    {
        return await _context.IncomeYearMonthlies
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }

    public async Task<List<IncomeYearTotal>> GetIncomeYearTotalAsync(int userId)
    {
        return await _context.IncomeYearTotals
            .Where(i => i.IdUser == userId)
            .ToListAsync();
    }
}
