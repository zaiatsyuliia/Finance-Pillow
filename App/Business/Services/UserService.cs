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
            var user = await _userRepository.GetByLoginAsync(login);

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
            var existingUser = await _userRepository.GetByLoginAsync(login);

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
            var user = await _userRepository.GetByLoginAsync(login);

            if (user == null)
            {
                return null;
            }

            UserDto dto = new UserDto
            {
                IdUser = user.IdUser,
                Login = user.Login,
                Password = user.Password, // Note: You may want to avoid sending the password back to the client
                ExpenseLimit = user.ExpenseLimit,
                IncomeLimit = user.IncomeLimit
            };

            return dto;
        }

        public async Task<UserDto> GetByUserId(int userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            UserDto dto = new UserDto
            {
                IdUser = user.IdUser,
                Login = user.Login,
                Password = user.Password, // Note: You may want to avoid sending the password back to the client
                ExpenseLimit = user.ExpenseLimit,
                IncomeLimit = user.IncomeLimit
            };

            return dto;
        }

        public async Task<bool> ChangeSettings(UserDto userDto)
        {
            var user = await _userRepository.GetByUserIdAsync(userDto.IdUser);

            if (user == null)
            {
                return false; // User not found
            }

            // Update the user's settings
            if (userDto.Login != null)
            {
                user.Login = userDto.Login;
            }
            if (userDto.Password != null)
            {
                user.Password = userDto.Password;
            }
            if (userDto.ExpenseLimit != null)
            {
                user.ExpenseLimit = userDto.ExpenseLimit;
            }
            if (userDto.IncomeLimit != null)
            {
                user.IncomeLimit = userDto.IncomeLimit;
            }

            // Save changes to the database
            await _userRepository.SaveChangesAsync();

            return true; // Settings updated successfully
        }
    }

}
