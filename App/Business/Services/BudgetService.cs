using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTO;
using Data.Models;
using Data.Repositories;

namespace Business.Services;

public class BudgetService
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetService(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<double> GetUserBudgetAsync(string userId)
    {
        // Default to 0 if no budget is found
        return await _budgetRepository.GetUserBudgetAsync(userId) ?? 0;
    }

    public async Task<LimitsDto> GetLimitsAsync(string userId)
    {
        var limits = await _budgetRepository.GetLimitsAsync(userId);

        if (limits == null)
            return null;

        return new LimitsDto
        {
            UserId = userId,
            TotalExpense = limits.TotalExpense ?? 0,
            ExpenseLimit = limits.ExpenseLimit ?? 0,
            ExpenseLimitExceeded = limits.ExpenseLimitExceeded ?? false,
            TotalIncome = limits.TotalIncome ?? 0,
            IncomeLimit = limits.IncomeLimit ?? 0,
            IncomeLimitExceeded = limits.IncomeLimitExceeded ?? false
        };
    }

    public async Task<List<HistoryDto>> GetUserHistoryAsync(string userId)
    {
        var histories = await _budgetRepository.GetUserHistoryAsync(userId);

        return histories.Select(h => new HistoryDto
        {
            TransactionId = h.TransactionId ?? 0,
            TransactionType = h.TransactionType,
            Category = h.Category,
            Date = h.Time ?? DateTime.MinValue,
            Sum = h.Sum ?? 0,
            Details = h.Details ?? "",
        }).ToList();
    }

    public async Task<List<HistoryDto>> GetUserHistoryTimeAsync(string userId, DateTime startDate, DateTime endDate)
    {
        var histories = await _budgetRepository.GetUserHistoryTimeAsync(userId, startDate, endDate);

        return histories.Select(h => new HistoryDto
        {
            TransactionId = h.TransactionId ?? 0,
            TransactionType = h.TransactionType,
            Category = h.Category,
            Date = h.Time ?? DateTime.MinValue,
            Sum = h.Sum ?? 0
        }).ToList();
    }

    public async Task<List<HistoryDto>> GetUserExpenseHistoryAsync(string userId)
    {
        var histories = await _budgetRepository.GetUserExpenseHistoryAsync(userId);

        return histories.Select(h => new HistoryDto
        {
            TransactionId = h.TransactionId ?? 0,
            TransactionType = h.TransactionType,
            Category = h.Category,
            Date = h.Time ?? DateTime.MinValue,
            Sum = h.Sum ?? 0
        }).ToList();
    }

    public async Task<List<HistoryDto>> GetUserIncomeHistoryAsync(string userId)
    {
        var histories = await _budgetRepository.GetUserIncomeHistoryAsync(userId);

        return histories.Select(h => new HistoryDto
        {
            TransactionId = h.TransactionId ?? 0,
            TransactionType = h.TransactionType,
            Category = h.Category,
            Date = h.Time ?? DateTime.MinValue,
            Sum = h.Sum ?? 0
        }).ToList();
    }
}
