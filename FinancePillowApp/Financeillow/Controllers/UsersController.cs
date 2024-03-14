using Financeillow.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Financeillow.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        private IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Database page called");
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        public IActionResult Test()
        {
            _logger.LogInformation("Test page called");
            return View();
        }
    }
}
