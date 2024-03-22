using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IUserBudgetRepository
{
    Task<double?> GetUserBudgetAsync(int userId);
}

public class UserBudgetRepository : IUserBudgetRepository
{
    private readonly Context _context;

    public UserBudgetRepository(Context context)
    {
        _context = context;
    }

    public async Task<double?> GetUserBudgetAsync(int userId)
    {
        var userBudget = await _context.UserBudgets
                .Where(u => u.IdUser == userId)
                .Select(u => u.Budget)
                .FirstOrDefaultAsync();

        return userBudget;
    }
}