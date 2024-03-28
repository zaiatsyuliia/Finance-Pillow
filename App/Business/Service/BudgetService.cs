using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Data.Repositories;

namespace Business.Service
{
    public class BudgetService
    {
        private readonly IUserBudgetRepository _userBudgetRepository;
        private readonly IHistoryRepository _historyRepository;

        public BudgetService(IUserBudgetRepository userBudgetRepository, IHistoryRepository historyRepository)
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
    }
}
