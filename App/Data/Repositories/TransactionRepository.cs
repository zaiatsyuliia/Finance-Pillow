using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ITransactionRepository
{
    Task AddExpenseAsync(string userId, int categoryId, DateTime time, double sum);

    Task AddIncomeAsync(string userId, int categoryId, DateTime time, double sum);

    Task DeleteIncomeAsync(int incomeId);

    Task DeleteExpenseAsync(int expenseId);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly FPDbContext _context;

    public TransactionRepository(FPDbContext context)
    {
        _context = context;
    }

    public async Task AddExpenseAsync(string userId, int categoryId, DateTime time, double sum)
    {
        var expense = new Expense
        {
            UserId = userId, // Ensure this is the correct foreign key to AspNetUsers
            ExpenseCategoryId = categoryId, // Correct name as per your database schema
            Time = time,
            Sum = sum
        };

        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();
    }

    public async Task AddIncomeAsync(string userId, int categoryId, DateTime time, double sum)
    {
        var income = new Income
        {
            UserId = userId, // Ensure this is the correct foreign key to AspNetUsers
            IncomeCategoryId = categoryId, // Correct name as per your database schema
            Time = time,
            Sum = sum
        };

        _context.Incomes.Add(income);

        await _context.SaveChangesAsync();
    }
    public async Task DeleteIncomeAsync(int incomeId)
    {
        var income = await _context.Incomes.FindAsync(incomeId);
        if (income != null)
        {
            _context.Incomes.Remove(income);

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteExpenseAsync(int expenseId)
    {
        var expense = await _context.Expenses.FindAsync(expenseId);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);

            await _context.SaveChangesAsync();
        }
    }
}