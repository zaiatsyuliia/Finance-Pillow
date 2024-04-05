using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByLoginAsync(string login);

        Task<User> GetByUserIdAsync(int id);

        Task AddUserAsync(string login, string password);

        Task SaveChangesAsync();
    }

    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User> GetByUserIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(string login, string password)
        {
            var user = new User { Login = login, Password = password }; // Replace User with your actual user entity class
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
