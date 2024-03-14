using Financeillow.Data.Repositories;
using Financeillow.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financeillow.Presentation.Models;

public class UserRepository : IUserRepository
{
    private readonly MyContext _context;

    public UserRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Users> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    // ... (реалізація інших методів CRUD)
}
