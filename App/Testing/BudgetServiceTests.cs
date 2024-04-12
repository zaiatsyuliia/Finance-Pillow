using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Business.DTO;
using Data.Repositories;
using Business.Services;

namespace Testing;

public class BudgetServiceTests
{
    private readonly Mock<IBudgetRepository> _mockBudgetRepository;
    private readonly Mock<IHistoryRepository> _mockHistoryRepository;
    private readonly BudgetService _budgetService;

    public BudgetServiceTests()
    {
        _mockBudgetRepository = new Mock<IBudgetRepository>();
        _mockHistoryRepository = new Mock<IHistoryRepository>();
        _budgetService = new BudgetService(_mockBudgetRepository.Object, _mockHistoryRepository.Object);
    }

    [Fact]
    public async Task GetUserBudgetAsync_ReturnsExpectedBudget()
    {
        // Arrange
        _mockBudgetRepository.Setup(repo => repo.GetUserBudgetAsync(It.IsAny<int>()))
                              .ReturnsAsync(200.0);

        // Act
        var result = await _budgetService.GetUserBudgetAsync(1);

        // Assert
        Assert.Equal(200.0, result);
    }

    [Fact]
    public async Task GetUserHistoryAsync_ReturnsFormattedHistory()
    {
        // Arrange
        var historyData = new List<Data.Models.History>
        {
            new Data.Models.History { TransactionType = "Expense", Category = "Food", Time = DateTime.Now, Sum = 50 },
            new Data.Models.History { TransactionType = "Income", Category = "Salary", Time = DateTime.Now.AddDays(-1), Sum = 150 }
        };
        _mockHistoryRepository.Setup(repo => repo.GetUserHistoryAsync(It.IsAny<int>()))
                              .ReturnsAsync(historyData);

        // Act
        var result = await _budgetService.GetUserHistoryAsync(1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Expense", result[0].TransactionType);
        Assert.Equal("Food", result[0].Category);
        Assert.Equal(50, result[0].Sum);
        Assert.True(result[0].Date.Date == DateTime.Now.Date);
    }

    [Fact]
    public async Task GetExpenseLimitAsync_ReturnsCorrectLimit()
    {
        // Arrange
        var limitData = new Data.Models.ExpenseMonthLimitComparison
        {
            TotalSum = 500,
            UserLimit = 1000,
            LimitStatus = false
        };

        _mockBudgetRepository.Setup(repo => repo.GetExpenseMonthLimitComparisonAsync(It.IsAny<int>()))
                              .ReturnsAsync(limitData);

        // Act
        var result = await _budgetService.GetExpenseLimitAsync(1);

        // Assert
        Assert.Equal(500, result.TotalSum);
        Assert.Equal(1000, result.UserLimit);
        Assert.False(result.LimitStatus);
    }

    [Fact]
    public async Task GetIncomeLimitAsync_ReturnsCorrectLimit()
    {
        // Arrange
        var limitData = new Data.Models.IncomeMonthLimitComparison
        {
            TotalSum = 2500,
            UserLimit = 2000,
            LimitStatus = true
        };

        _mockBudgetRepository.Setup(repo => repo.GetIncomeMonthLimitComparisonAsync(It.IsAny<int>()))
                              .ReturnsAsync(limitData);

        // Act
        var result = await _budgetService.GetIncomeLimitAsync(1);

        // Assert
        Assert.Equal(2500, result.TotalSum);
        Assert.Equal(2000, result.UserLimit);
        Assert.True(result.LimitStatus);
    }
}
