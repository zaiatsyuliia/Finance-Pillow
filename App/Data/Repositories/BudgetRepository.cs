using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IBudgetRepository
    {
        Task<double?> GetUserBudgetAsync(int userId);

        Task<ExpenseMonthLimitComparison> GetExpenseMonthLimitComparisonAsync(int userId);

        Task<IncomeMonthLimitComparison> GetIncomeMonthLimitComparisonAsync(int userId);

        Task DeleteIncomeAsync(int idIncome);

        Task DeleteExpenseAsync(int idExpense);
    }

    public class BudgetRepository : IBudgetRepository
    {
        private readonly Context _context;

        public BudgetRepository(Context context)
        {
            _context = context;
        }

        public async Task<double?> GetUserBudgetAsync(int userId)
        {
            var userBudget = await _context.UserBudgets
                    .Where(u => u.IdUser == userId)
                    .Select(u => u.Budget)
                    .FirstOrDefaultAsync();

            return userBudget;
        }

        public async Task<ExpenseMonthLimitComparison> GetExpenseMonthLimitComparisonAsync(int userId)
        {
            return await _context.ExpenseMonthLimitComparison
                .FirstOrDefaultAsync(e => e.IdUser == userId);
        }

        public async Task<IncomeMonthLimitComparison> GetIncomeMonthLimitComparisonAsync(int userId)
        {
            return await _context.IncomeMonthLimitComparison
                .FirstOrDefaultAsync(e => e.IdUser == userId);
        }

        public async Task DeleteIncomeAsync(int idIncome)
        {
            var income = await _context.Incomes.FindAsync(idIncome);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExpenseAsync(int idExpense)
        {
            var expense = await _context.Expenses.FindAsync(idExpense);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
