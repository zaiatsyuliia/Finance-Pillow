using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace Business
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            return await _categoryRepository.GetExpenseCategoryListAsync();
        }

        public async Task<List<IncomeCategory>> GetIncomeCategoriesAsync()
        {
            return await _categoryRepository.GetIncomeCategoryListAsync();
        }
    }
}
