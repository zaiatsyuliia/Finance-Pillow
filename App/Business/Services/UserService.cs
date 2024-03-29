using Business.DTO;
using Data.Repositories;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            // Check if the user exists in the database
            var user = await _userRepository.GetUserByLoginAsync(login);
            
            if (user == null)
            {
                return false; // User not found
            }

            // Compare the password with the one stored in the database
            return user.Password == password;
        }

        public async Task<bool> RegisterAsync(string login, string password)
        {
            // Check if the user already exists in the database
            var existingUser = await _userRepository.GetUserByLoginAsync(login);

            if (existingUser != null)
            {
                return false; // User already exists
            }

            // Add the new user to the database
            await _userRepository.AddUserAsync(login, password);
            return true; // Registration successful
        }

        public async Task<UserDto> GetByLogin(string login)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            UserDto dto = new UserDto
            {
                IdUser = user.IdUser,
                Login = user.Login,
                Password = user.Password
            };

            return dto;
        }
    }
}
