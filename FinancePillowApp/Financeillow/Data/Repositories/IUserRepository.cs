using Financeillow.Presentation.Models;

namespace Financeillow.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();

        Task<Users> GetByIdAsync(int id);
    }

}
