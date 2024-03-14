using Financeillow.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Financeillow.Presentation.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View("/Views/Home/Index.cshtml");
        }
    }

}
