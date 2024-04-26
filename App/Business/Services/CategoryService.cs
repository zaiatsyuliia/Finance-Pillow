using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Data.Repositories;

namespace Business.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetExpenseCategoriesAsync()
        {
            var expenseCategories = await _categoryRepository.GetExpenseCategoryListAsync();

            var categoryDtoList = new List<CategoryDto>();
            foreach (var category in expenseCategories)
            {
                categoryDtoList.Add(new CategoryDto
                {
                    CategoryId = category.ExpenseCategoryId,
                    Name = category.Name
                });
            }
            return categoryDtoList;
        }

        public async Task<List<CategoryDto>> GetIncomeCategoriesAsync()
        {
            var incomeCategories = await _categoryRepository.GetIncomeCategoryListAsync();

            var categoryDtoList = new List<CategoryDto>();
            foreach (var category in incomeCategories)
            {
                categoryDtoList.Add(new CategoryDto
                {
                    CategoryId = category.IncomeCategoryId,
                    Name = category.Name
                });
            }
            return categoryDtoList;
        }
    }
}