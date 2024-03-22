using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace Business.Services
{
    public class BudgetService
    {
        private readonly IUserBudgetRepository _userBudgetRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHistoryRepository _historyRepository;

        public BudgetService(IUserBudgetRepository userBudgetRepository, ITransactionRepository transactionRepository, IHistoryRepository historyRepository)
        {
            _userBudgetRepository = userBudgetRepository;
            _transactionRepository = transactionRepository;
            _historyRepository = historyRepository;
        }

        public async Task<double?> GetUserBudgetAsync(int userId)
        {
            return await _userBudgetRepository.GetUserBudgetAsync(userId);
        }

        public async Task AddExpenseAsync(int userId, int categoryId, DateTime time, double sum)
        {
            await _transactionRepository.AddExpenseAsync(userId, categoryId, time, sum);
        }

        public async Task AddIncomeAsync(int userId, int categoryId, DateTime time, double sum)
        {
            await _transactionRepository.AddIncomeAsync(userId, categoryId, time, sum);
        }

        public async Task<List<History>> GetUserHistoryAsync(int userId)
        {
            return await _historyRepository.GetUserHistoryAsync(userId);
        }
    }
}
