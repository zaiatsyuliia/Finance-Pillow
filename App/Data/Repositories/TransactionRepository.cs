using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public interface ITransactionRepository
{
    Task AddExpenseAsync(int userId, int categoryId, DateTime time, double sum);
    Task AddIncomeAsync(int userId, int categoryId, DateTime time, double sum);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly MyContext _context;

    public TransactionRepository(MyContext context)
    {
        _context = context;
    }

    public async Task AddExpenseAsync(int userId, int categoryId, DateTime time, double sum)
    {
        var expense = new Expense
        {
            IdUser = userId,
            IdCategoryExpense = categoryId,
            Time = time,
            Sum = sum
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }

    public async Task AddIncomeAsync(int userId, int categoryId, DateTime time, double sum)
    {
        var income = new Income
        {
            IdUser = userId,
            IdCategoryIncome = categoryId,
            Time = time,
            Sum = sum
        };

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync();
    }
}