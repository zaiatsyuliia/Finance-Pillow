using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Business.DTO;
using Data.Models;
using Data.Repositories;
using Business.Services;

namespace Testing
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetExpenseCategoriesAsync_ReturnsExpenseCategories()
        {
            // Arrange
            var expenseCategories = new List<ExpenseCategory>
            {
                new ExpenseCategory { ExpenseCategoryId = 1, Name = "Food" },
                new ExpenseCategory { ExpenseCategoryId = 2, Name = "Transportation" }
            };

            _mockCategoryRepository.Setup(repo => repo.GetExpenseCategoryListAsync())
                                   .ReturnsAsync(expenseCategories);

            // Act
            var result = await _categoryService.GetExpenseCategoriesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].CategoryId);
            Assert.Equal("Food", result[0].Name);
            Assert.Equal(2, result[1].CategoryId);
            Assert.Equal("Transportation", result[1].Name);
        }

        [Fact]
        public async Task GetIncomeCategoriesAsync_ReturnsIncomeCategories()
        {
            // Arrange
            var incomeCategories = new List<IncomeCategory>
            {
                new IncomeCategory { IncomeCategoryId = 1, Name = "Salary" },
                new IncomeCategory { IncomeCategoryId = 2, Name = "Investment" }
            };

            _mockCategoryRepository.Setup(repo => repo.GetIncomeCategoryListAsync())
                                   .ReturnsAsync(incomeCategories);

            // Act
            var result = await _categoryService.GetIncomeCategoriesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].CategoryId);
            Assert.Equal("Salary", result[0].Name);
            Assert.Equal(2, result[1].CategoryId);
            Assert.Equal("Investment", result[1].Name);
        }

        [Fact]
        public async Task GetExpenseCategoriesAsync_ReturnsEmptyListWhenNoCategories()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetExpenseCategoryListAsync())
                                   .ReturnsAsync(new List<ExpenseCategory>());

            // Act
            var result = await _categoryService.GetExpenseCategoriesAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetIncomeCategoriesAsync_ReturnsEmptyListWhenNoCategories()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetIncomeCategoryListAsync())
                                   .ReturnsAsync(new List<IncomeCategory>());

            // Act
            var result = await _categoryService.GetIncomeCategoriesAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
