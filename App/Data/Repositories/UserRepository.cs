using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<User> GetByIdAsync(int id);
}

public class UserRepository : IUserRepository
{
    private readonly MyContext _context;

    public UserRepository(MyContext context)
    {
#pragma warning disable SA1101 // Prefix local calls with this
        _context = context;
#pragma warning restore SA1101 // Prefix local calls with this
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
#pragma warning disable SA1101 // Prefix local calls with this
        return await _context.Users.ToListAsync();
#pragma warning restore SA1101 // Prefix local calls with this
    }

    public async Task<User> GetByIdAsync(int id)
    {
#pragma warning disable SA1101 // Prefix local calls with this
        return await _context.Users.FindAsync(id);
#pragma warning restore SA1101 // Prefix local calls with this
    }
}
