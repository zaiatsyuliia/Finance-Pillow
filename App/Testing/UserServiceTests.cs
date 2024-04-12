using Xunit;
using Moq;
using System.Threading.Tasks;
using Business.Services;
using Data.Repositories;
using Data.Models;
using Business.DTO;

namespace Testing
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object);
        }

        [Theory]
        [InlineData("user1", "pass123", true)]
        [InlineData("user2", "wrongpass", false)]
        public async Task LoginAsync_ReturnsExpectedResult(string login, string password, bool expectedResult)
        {
            // Arrange
            var user = expectedResult ? new User { Login = login, Password = password } : null;
            _mockRepo.Setup(x => x.GetByLoginAsync(login)).ReturnsAsync(user);

            // Act
            var result = await _userService.LoginAsync(login, password);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task RegisterAsync_UserExists_ReturnsFalse()
        {
            // Arrange
            var login = "existingUser";
            _mockRepo.Setup(x => x.GetByLoginAsync(login)).ReturnsAsync(new User());

            // Act
            var result = await _userService.RegisterAsync(login, "password123");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RegisterAsync_NewUser_ReturnsTrue()
        {
            // Arrange
            var login = "newUser";
            _mockRepo.Setup(x => x.GetByLoginAsync(login)).ReturnsAsync((User)null);
            _mockRepo.Setup(x => x.AddUserAsync(login, "password123")).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.RegisterAsync(login, "password123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetByLogin_UserExists_ReturnsUserDto()
        {
            // Arrange
            var login = "user1";
            var user = new User { IdUser = 1, Login = login, Password = "hidden", ExpenseLimit = 1000, IncomeLimit = 2000 };
            _mockRepo.Setup(x => x.GetByLoginAsync(login)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetByLogin(login);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(login, result.Login);
            Assert.Equal(user.ExpenseLimit, result.ExpenseLimit);
        }

        [Fact]
        public async Task GetByLogin_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            string nonExistentLogin = "nonexistentuser";
            _mockRepo.Setup(x => x.GetByLoginAsync(nonExistentLogin)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetByLogin(nonExistentLogin);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByUserId_UserExists_ReturnsUserDto()
        {
            // Arrange
            int userId = 1;
            var user = new User { IdUser = userId, Login = "user1", Password = "hidden", ExpenseLimit = 1000, IncomeLimit = 2000 };
            _mockRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.IdUser);
            Assert.Equal(user.Login, result.Login);
            Assert.Equal(user.ExpenseLimit, result.ExpenseLimit);
            Assert.Equal(user.IncomeLimit, result.IncomeLimit);
        }

        [Fact]
        public async Task GetByUserId_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetByUserIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetByUserId(99);

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task ChangeSettings_UserExists_UpdatesSettings()
        {
            // Arrange
            var userDto = new UserDto { IdUser = 1, Login = "newLogin", Password = "newPassword", ExpenseLimit = 1500, IncomeLimit = 2500 };
            var user = new User { IdUser = 1, Login = "user1", Password = "pass123", ExpenseLimit = 1000, IncomeLimit = 2000 };
            _mockRepo.Setup(x => x.GetByUserIdAsync(userDto.IdUser)).ReturnsAsync(user);
            _mockRepo.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.ChangeSettings(userDto);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.Equal(userDto.Login, user.Login);
            Assert.Equal(userDto.Password, user.Password);
            Assert.Equal(userDto.ExpenseLimit, user.ExpenseLimit);
            Assert.Equal(userDto.IncomeLimit, user.IncomeLimit);
        }

        [Fact]
        public async Task ChangeSettings_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var userDto = new UserDto { IdUser = 99 };
            _mockRepo.Setup(x => x.GetByUserIdAsync(userDto.IdUser)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.ChangeSettings(userDto);

            // Assert
            Assert.False(result);
            _mockRepo.Verify(x => x.SaveChangesAsync(), Times.Never);
        }
    }
}
