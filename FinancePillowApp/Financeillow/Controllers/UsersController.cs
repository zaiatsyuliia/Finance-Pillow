namespace Financeillow.Controllers
{
    using Financeillow.Data.Repositories;
    using Financeillow.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
