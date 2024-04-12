using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Data.Repositories;

namespace Business.Services
{
    public class BudgetService
    {
        private readonly IBudgetRepository _userBudgetRepository;
        private readonly IHistoryRepository _historyRepository;

        public BudgetService(IBudgetRepository userBudgetRepository, IHistoryRepository historyRepository)
        {
            _userBudgetRepository = userBudgetRepository;
            _historyRepository = historyRepository;
        }

        public async Task<double> GetUserBudgetAsync(int userId)
        {
            return await _userBudgetRepository.GetUserBudgetAsync(userId) ?? 0;
        }

        public async Task<List<HistoryDto>> GetUserHistoryAsync(int userId)
        {
            var historyList = await _historyRepository.GetUserHistoryAsync(userId);
            return historyList.Select(h => new HistoryDto
            {
                TransactionType = h.TransactionType,
                Category = h.Category,
                Date = h.Time.HasValue ? h.Time.Value.Date : DateTime.MinValue, // Extract only the date part
                Sum = h.Sum ?? 0
            }).ToList();
        }

        public async Task<LimitDTO> GetExpenseLimitAsync(int userId)
        {
            var limit = await _userBudgetRepository.GetExpenseMonthLimitComparisonAsync(userId);

            return new LimitDTO
            {
                TotalSum = limit.TotalSum,
                UserLimit = limit.UserLimit,
                LimitStatus = limit.LimitStatus
            };
        }
        public async Task<LimitDTO> GetIncomeLimitAsync(int userId)
        {
            var limit = await _userBudgetRepository.GetIncomeMonthLimitComparisonAsync(userId);

            return new LimitDTO
            {
                TotalSum = limit.TotalSum,
                UserLimit = limit.UserLimit,
                LimitStatus = limit.LimitStatus
            };
        }
    }
}