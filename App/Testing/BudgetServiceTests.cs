using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Business.DTO;
using Data.Repositories;
using Business.Services;

namespace Testing
{
    public class BudgetServiceTests
    {
        private readonly Mock<IBudgetRepository> _mockBudgetRepository;
        private readonly BudgetService _budgetService;

        public BudgetServiceTests()
        {
            _mockBudgetRepository = new Mock<IBudgetRepository>();
            _budgetService = new BudgetService(_mockBudgetRepository.Object);
        }

        [Fact]
        public async Task GetUserBudgetAsync_ReturnsExpectedBudget()
        {
            // Arrange
            _mockBudgetRepository.Setup(repo => repo.GetUserBudgetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(200.0);

            // Act
            var result = await _budgetService.GetUserBudgetAsync("user1");

            // Assert
            Assert.Equal(200.0, result);
        }

        [Fact]
        public async Task GetUserBudgetAsync_ReturnsZeroWhenNoBudgetFound()
        {
            // Arrange
            _mockBudgetRepository.Setup(repo => repo.GetUserBudgetAsync(It.IsAny<string>()))
                                  .ReturnsAsync((double?)null);

            // Act
            var result = await _budgetService.GetUserBudgetAsync("user1");

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public async Task GetLimitsAsync_ReturnsCorrectLimits()
        {
            // Arrange
            var limitsData = new Data.Models.Limit
            {
                TotalExpense = 500,
                ExpenseLimit = 1000,
                ExpenseLimitExceeded = false,
                TotalIncome = 1500,
                IncomeLimit = 2000,
                IncomeLimitExceeded = false
            };

            _mockBudgetRepository.Setup(repo => repo.GetLimitsAsync(It.IsAny<string>()))
                                  .ReturnsAsync(limitsData);

            // Act
            var result = await _budgetService.GetLimitsAsync("user1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.TotalExpense);
            Assert.Equal(1000, result.ExpenseLimit);
            Assert.False(result.ExpenseLimitExceeded);
            Assert.Equal(1500, result.TotalIncome);
            Assert.Equal(2000, result.IncomeLimit);
            Assert.False(result.IncomeLimitExceeded);
        }

        [Fact]
        public async Task GetLimitsAsync_ReturnsNullWhenNoLimitsFound()
        {
            // Arrange
            _mockBudgetRepository.Setup(repo => repo.GetLimitsAsync(It.IsAny<string>()))
                                  .ReturnsAsync((Data.Models.Limit)null);

            // Act
            var result = await _budgetService.GetLimitsAsync("user1");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserHistoryAsync_ReturnsFormattedHistory()
        {
            // Arrange
            var historyData = new List<Data.Models.History>
            {
                new Data.Models.History { TransactionId = 1, TransactionType = "Expense", Category = "Food", Time = DateTime.Now, Sum = 50 },
                new Data.Models.History { TransactionId = 2, TransactionType = "Income", Category = "Salary", Time = DateTime.Now.AddDays(-1), Sum = 150 }
            };

            _mockBudgetRepository.Setup(repo => repo.GetUserHistoryAsync(It.IsAny<string>()))
                                  .ReturnsAsync(historyData);

            // Act
            var result = await _budgetService.GetUserHistoryAsync("user1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal("Expense", result[0].TransactionType);
            Assert.Equal("Food", result[0].Category);
            Assert.Equal(50, result[0].Sum);
            Assert.True(result[0].Date.Date == DateTime.Now.Date);
        }

        [Fact]
        public async Task GetUserHistoryTimeAsync_ReturnsFilteredHistory()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now;
            var historyData = new List<Data.Models.History>
            {
                new Data.Models.History { TransactionId = 1, TransactionType = "Expense", Category = "Food", Time = DateTime.Now.AddDays(-6), Sum = 50 },
                new Data.Models.History { TransactionId = 2, TransactionType = "Income", Category = "Salary", Time = DateTime.Now.AddDays(-5), Sum = 150 }
            };

            _mockBudgetRepository.Setup(repo => repo.GetUserHistoryTimeAsync(It.IsAny<string>(), startDate, endDate))
                                  .ReturnsAsync(historyData);

            // Act
            var result = await _budgetService.GetUserHistoryTimeAsync("user1", startDate, endDate);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal("Expense", result[0].TransactionType);
            Assert.Equal("Food", result[0].Category);
            Assert.Equal(50, result[0].Sum);
            Assert.True(result[0].Date.Date == DateTime.Now.AddDays(-6).Date);
        }

        [Fact]
        public async Task GetUserExpenseHistoryAsync_ReturnsExpenseHistory()
        {
            // Arrange
            var expenseHistoryData = new List<Data.Models.History>
            {
                new Data.Models.History { TransactionId = 1, TransactionType = "Expense", Category = "Food", Time = DateTime.Now, Sum = 50 }
            };

            _mockBudgetRepository.Setup(repo => repo.GetUserExpenseHistoryAsync(It.IsAny<string>()))
                                  .ReturnsAsync(expenseHistoryData);

            // Act
            var result = await _budgetService.GetUserExpenseHistoryAsync("user1");

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal("Expense", result[0].TransactionType);
            Assert.Equal("Food", result[0].Category);
            Assert.Equal(50, result[0].Sum);
            Assert.True(result[0].Date.Date == DateTime.Now.Date);
        }

        [Fact]
        public async Task GetUserIncomeHistoryAsync_ReturnsIncomeHistory()
        {
            // Arrange
            var incomeHistoryData = new List<Data.Models.History>
            {
                new Data.Models.History { TransactionId = 1, TransactionType = "Income", Category = "Salary", Time = DateTime.Now, Sum = 150 }
            };

            _mockBudgetRepository.Setup(repo => repo.GetUserIncomeHistoryAsync(It.IsAny<string>()))
                                  .ReturnsAsync(incomeHistoryData);

            // Act
            var result = await _budgetService.GetUserIncomeHistoryAsync("user1");

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal("Income", result[0].TransactionType);
            Assert.Equal("Salary", result[0].Category);
            Assert.Equal(150, result[0].Sum);
            Assert.True(result[0].Date.Date == DateTime.Now.Date);
        }
    }
}
