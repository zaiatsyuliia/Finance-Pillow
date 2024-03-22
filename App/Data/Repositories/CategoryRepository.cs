using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public interface ICategoryRepository
{
    Task<List<ExpenseCategory>> GetExpenseCategoryListAsync();
    Task<List<IncomeCategory>> GetIncomeCategoryListAsync();
}

public class CategoryRepository : ICategoryRepository
{
    private readonly Context _context;

    public CategoryRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<ExpenseCategory>> GetExpenseCategoryListAsync()
    {
        return await _context.ExpenseCategories.ToListAsync();
    }

    public async Task<List<IncomeCategory>> GetIncomeCategoryListAsync()
    {
        return await _context.IncomeCategories.ToListAsync();
    }
}