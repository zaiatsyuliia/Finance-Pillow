using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Business.Services;
using Data.Repositories;
using Business.DTO;

namespace Testing;

public class TransactionServiceTests
{
    [Fact]
    public async Task AddExpenseAsync_CallsRepositoryWithCorrectParameters()
    {
        // Arrange
        var mockRepo = new Mock<ITransactionRepository>();
        var service = new TransactionService(mockRepo.Object);
        var userId = 1;
        var transactionDto = new TransactionDto
        {
            CategoryId = 10,
            Sum = 100.00
        };

        // Act
        await service.AddExpenseAsync(userId, transactionDto);

        // Assert
        mockRepo.Verify(repo => repo.AddExpenseAsync(userId, transactionDto.CategoryId, It.IsAny<DateTime>(), transactionDto.Sum), Times.Once());
    }

    [Fact]
    public async Task AddIncomeAsync_CallsRepositoryWithCorrectParameters()
    {
        // Arrange
        var mockRepo = new Mock<ITransactionRepository>();
        var service = new TransactionService(mockRepo.Object);
        var userId = 1;
        var transactionDto = new TransactionDto
        {
            CategoryId = 20,
            Sum = 200.00
        };

        // Act
        await service.AddIncomeAsync(userId, transactionDto);

        // Assert
        mockRepo.Verify(repo => repo.AddIncomeAsync(userId, transactionDto.CategoryId, It.IsAny<DateTime>(), transactionDto.Sum), Times.Once());
    }
}
