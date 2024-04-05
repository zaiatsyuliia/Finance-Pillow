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

        public async Task<BudgetDto> GetUserBudgetAsync(int userId)
        {
            double? budget = await _userBudgetRepository.GetUserBudgetAsync(userId);
            return new BudgetDto { Sum = budget };
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

        public async Task<LimitDTO> GetLimitAsync(int userId)
        {
            // Call repository method to retrieve data
            var limit = await _userBudgetRepository.GetExpenseMonthLimitComparisonAsync(userId);

            // Map the retrieved data to DTOs
            return new LimitDTO
            {
                TotalSum = limit.TotalSum,
                UserLimit = limit.UserLimit,
                LimitStatus = limit.LimitStatus
            };
        }
    }
}
