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
            return await _context.ExpenseMonthLimitComparisons
                .FirstOrDefaultAsync(e => e.IdUser == userId);
        }
    }
}
