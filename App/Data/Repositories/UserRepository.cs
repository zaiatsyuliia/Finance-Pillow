using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class UserRepository
{
    private readonly MyContext _context;

    public UserRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
