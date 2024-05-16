using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Business.Services;
using Data.Repositories;
using Business.DTO;
using Data.Models;

namespace Testing
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_mockTransactionRepository.Object);
        }

        [Fact]
        public async Task AddExpenseAsync_CallsRepositoryWithCorrectParameters()
        {
            // Arrange
            var userId = "abc";
            var transactionDto = new TransactionDto
            {
                Type = TransactionType.Expense,
                CategoryId = 10,
                Sum = 100.00
            };

            // Act
            await _transactionService.AddTransactionAsync(userId, transactionDto);

            // Assert
            _mockTransactionRepository.Verify(repo => repo.AddExpenseAsync(userId, transactionDto.CategoryId, It.IsAny<DateTime>(), transactionDto.Sum), Times.Once());
        }

        [Fact]
        public async Task AddIncomeAsync_CallsRepositoryWithCorrectParameters()
        {
            // Arrange
            var userId = "abc";
            var transactionDto = new TransactionDto
            {
                Type = TransactionType.Income,
                CategoryId = 20,
                Sum = 200.00
            };

            // Act
            await _transactionService.AddTransactionAsync(userId, transactionDto);

            // Assert
            _mockTransactionRepository.Verify(repo => repo.AddIncomeAsync(userId, transactionDto.CategoryId, It.IsAny<DateTime>(), transactionDto.Sum), Times.Once());
        }

        [Fact]
        public async Task AddTransactionAsync_ThrowsExceptionForInvalidTransactionType()
        {
            // Arrange
            var userId = "abc";
            var transactionDto = new TransactionDto
            {
                Type = (TransactionType)99, // Invalid transaction type
                CategoryId = 30,
                Sum = 300.00
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _transactionService.AddTransactionAsync(userId, transactionDto));
        }

        [Fact]
        public async Task DeleteIncomeAsync_CallsRepositoryWithCorrectParameters()
        {
            // Arrange
            var transactionId = 1;

            // Act
            await _transactionService.DeleteTransactionAsync(TransactionType.Income, transactionId);

            // Assert
            _mockTransactionRepository.Verify(repo => repo.DeleteIncomeAsync(transactionId), Times.Once());
        }

        [Fact]
        public async Task DeleteExpenseAsync_CallsRepositoryWithCorrectParameters()
        {
            // Arrange
            var transactionId = 1;

            // Act
            await _transactionService.DeleteTransactionAsync(TransactionType.Expense, transactionId);

            // Assert
            _mockTransactionRepository.Verify(repo => repo.DeleteExpenseAsync(transactionId), Times.Once());
        }

        [Fact]
        public async Task DeleteTransactionAsync_ThrowsExceptionForInvalidTransactionType()
        {
            // Arrange
            var transactionId = 1;
            var invalidTransactionType = (TransactionType)99; // Invalid transaction type

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _transactionService.DeleteTransactionAsync(invalidTransactionType, transactionId));
        }
    }
}
