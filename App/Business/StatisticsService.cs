using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace Business
{
    public class StatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<List<ExpenseMonthDaily>> GetExpenseMonthDailyAsync(int userId)
        {
            return await _statisticsRepository.GetExpenseMonthDailyAsync(userId);
        }

        public async Task<List<ExpenseMonthTotal>> GetExpenseMonthTotalAsync(int userId)
        {
            return await _statisticsRepository.GetExpenseMonthTotalAsync(userId);
        }

        public async Task<List<Expense6MonthsMonthly>> GetExpense6MonthsMonthlyAsync(int userId)
        {
            return await _statisticsRepository.GetExpense6MonthsMonthlyAsync(userId);
        }

        public async Task<List<Expense6MonthsTotal>> GetExpense6MonthsTotalAsync(int userId)
        {
            return await _statisticsRepository.GetExpense6MonthsTotalAsync(userId);
        }

        public async Task<List<ExpenseYearMonthly>> GetExpenseYearMonthlyAsync(int userId)
        {
            return await _statisticsRepository.GetExpenseYearMonthlyAsync(userId);
        }

        public async Task<List<ExpenseYearTotal>> GetExpenseYearTotalAsync(int userId)
        {
            return await _statisticsRepository.GetExpenseYearTotalAsync(userId);
        }

        public async Task<List<IncomeMonthDaily>> GetIncomeMonthDailyAsync(int userId)
        {
            return await _statisticsRepository.GetIncomeMonthDailyAsync(userId);
        }

        public async Task<List<IncomeMonthTotal>> GetIncomeMonthTotalAsync(int userId)
        {
            return await _statisticsRepository.GetIncomeMonthTotalAsync(userId);
        }

        public async Task<List<Income6MonthsMonthly>> GetIncome6MonthsMonthlyAsync(int userId)
        {
            return await _statisticsRepository.GetIncome6MonthsMonthlyAsync(userId);
        }

        public async Task<List<Income6MonthsTotal>> GetIncome6MonthsTotalAsync(int userId)
        {
            return await _statisticsRepository.GetIncome6MonthsTotalAsync(userId);
        }

        public async Task<List<IncomeYearMonthly>> GetIncomeYearMonthlyAsync(int userId)
        {
            return await _statisticsRepository.GetIncomeYearMonthlyAsync(userId);
        }

        public async Task<List<IncomeYearTotal>> GetIncomeYearTotalAsync(int userId)
        {
            return await _statisticsRepository.GetIncomeYearTotalAsync(userId);
        }
    }
}
