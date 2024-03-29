using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Data.Models;
using Data.Repositories;

namespace Business.Services
{
    public class StatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<List<StatisticsDailyDto>> GetExpenseMonthDailyAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpenseMonthDailyAsync(userId);

            var dtoList = new List<StatisticsDailyDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsDailyDto
                {
                    Day = expense.Day.HasValue ? expense.Day.Value.Date : DateTime.MinValue,
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsTotalDto>> GetExpenseMonthTotalAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpenseMonthTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsMonthlyDto>> GetExpense6MonthsMonthlyAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpense6MonthsMonthlyAsync(userId);

            var dtoList = new List<StatisticsMonthlyDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsMonthlyDto
                {
                    Month = expense.Month.HasValue ? expense.Month.Value.Date : DateTime.MinValue,
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsTotalDto>> GetExpense6MonthsTotalAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpense6MonthsTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsMonthlyDto>> GetExpenseYearMonthlyAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpenseYearMonthlyAsync(userId);

            var dtoList = new List<StatisticsMonthlyDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsMonthlyDto
                {
                    Month = expense.Month.HasValue ? expense.Month.Value.Date : DateTime.MinValue,
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;

        }

        public async Task<List<StatisticsTotalDto>> GetExpenseYearTotalAsync(int userId)
        {
            var expenses = await _statisticsRepository.GetExpenseYearTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var expense in expenses)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = expense.CategoryName,
                    TotalSum = expense.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsDailyDto>> GetIncomeMonthDailyAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncomeMonthDailyAsync(userId);

            var dtoList = new List<StatisticsDailyDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsDailyDto
                {
                    Day = income.Day.HasValue ? income.Day.Value.Date : DateTime.MinValue,
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsTotalDto>> GetIncomeMonthTotalAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncomeMonthTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;

        }

        public async Task<List<StatisticsMonthlyDto>> GetIncome6MonthsMonthlyAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncome6MonthsMonthlyAsync(userId);

            var dtoList = new List<StatisticsMonthlyDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsMonthlyDto
                {
                    Month = income.Month.HasValue ? income.Month.Value.Date : DateTime.MinValue,
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsTotalDto>> GetIncome6MonthsTotalAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncome6MonthsTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsMonthlyDto>> GetIncomeYearMonthlyAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncomeYearMonthlyAsync(userId);

            var dtoList = new List<StatisticsMonthlyDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsMonthlyDto
                {
                    Month = income.Month.HasValue ? income.Month.Value.Date : DateTime.MinValue,
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public async Task<List<StatisticsTotalDto>> GetIncomeYearTotalAsync(int userId)
        {
            var incomes = await _statisticsRepository.GetIncomeYearTotalAsync(userId);

            var dtoList = new List<StatisticsTotalDto>();
            foreach (var income in incomes)
            {
                var dto = new StatisticsTotalDto
                {
                    CategoryName = income.CategoryName,
                    TotalSum = income.TotalSum ?? 0
                };
                dtoList.Add(dto);
            }

            return dtoList;
        }


    }
}
