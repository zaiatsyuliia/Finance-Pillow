namespace Financeillow.Data.Repositories
{
    using Financeillow.Data.Models;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);
    }
}
