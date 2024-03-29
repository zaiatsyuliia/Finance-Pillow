using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Business.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Data.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly BudgetService _budgetService;
        private readonly TransactionService _transactionService;
        private readonly CategoryService _categoryService;
        private readonly StatisticsService _statisticsService;
        private readonly UserService _userService;
        private int userId;

        public HomeController(BudgetService budgetService,
               TransactionService transactionService,
               CategoryService categoryService,
               StatisticsService statisticsService,
               UserService userService)
        {
            _budgetService = budgetService;
            _transactionService = transactionService;
            _categoryService = categoryService;
            _statisticsService = statisticsService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            // Отримуємо userId з TempData
            if (TempData["UserId"] is int userId)
            {
                Console.WriteLine($"Logged in user ID: {userId}");

                var userBudget = await _budgetService.GetUserBudgetAsync(userId);
                var userHistory = await _budgetService.GetUserHistoryAsync(userId);
                var expenseCategories = await _categoryService.GetExpenseCategoriesAsync();
                var incomeCategories = await _categoryService.GetIncomeCategoriesAsync();

                var model = new HomeViewModel
                {
                    Budget = userBudget,
                    History = userHistory,
                    ExpenseCategories = expenseCategories,
                    IncomeCategories = incomeCategories
                };

                return View(model);
            }
            else
            {
                // Якщо userId відсутній, перенаправляємо користувача на сторінку входу
                return RedirectToAction(nameof(LoginPage));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionViewModel model)
        {
            if (ModelState.IsValid && (TempData["UserId"] is int userId))
            {
                var transactionDto = new TransactionDto
                {
                    Type = model.Type,
                    CategoryId = model.CategoryId,
                    Sum = model.Sum
                };

                if (model.Type == "Expense")
                {
                    await _transactionService.AddExpenseAsync(userId, transactionDto);
                }
                else if (model.Type == "Income")
                {
                    await _transactionService.AddIncomeAsync(userId, transactionDto);
                }
                else
                {
                    return BadRequest("Invalid transaction type.");
                }

                return RedirectToAction(nameof(Index));
            }

            return View("Index", model);
        }

        public async Task<IActionResult> Expenses()
        {
            var monthDaily = await _statisticsService.GetExpenseMonthDailyAsync(userId);
            var monthTotal = await _statisticsService.GetExpenseMonthTotalAsync(userId);
            var sixMonthsMonthly = await _statisticsService.GetExpense6MonthsMonthlyAsync(userId);
            var sixMonthsTotal = await _statisticsService.GetExpense6MonthsTotalAsync(userId);
            var yearMonthly = await _statisticsService.GetExpenseYearMonthlyAsync(userId);
            var yearTotal = await _statisticsService.GetExpenseYearTotalAsync(userId);
            var categories = await _categoryService.GetExpenseCategoriesAsync();

            var model = new StatisticsViewModel
            {
                MonthDaily = monthDaily,
                MonthTotal = monthTotal,
                SixMonthsMonthly = sixMonthsMonthly,
                SixMonthsTotal = sixMonthsTotal,
                YearMonthly = yearMonthly,
                YearTotal = yearTotal,
                Categories = categories
            };

            return View(model);
        }

        public async Task<IActionResult> Incomes()
        {
            if (TempData["UserId"] is int userId)
            {
                var monthDaily = await _statisticsService.GetIncomeMonthDailyAsync(userId);
                var monthTotal = await _statisticsService.GetIncomeMonthTotalAsync(userId);
                var sixMonthsMonthly = await _statisticsService.GetIncome6MonthsMonthlyAsync(userId);
                var sixMonthsTotal = await _statisticsService.GetIncome6MonthsTotalAsync(userId);
                var yearMonthly = await _statisticsService.GetIncomeYearMonthlyAsync(userId);
                var yearTotal = await _statisticsService.GetIncomeYearTotalAsync(userId);
                var categories = await _categoryService.GetIncomeCategoriesAsync();

                var model = new StatisticsViewModel
                {
                    MonthDaily = monthDaily,
                    MonthTotal = monthTotal,
                    SixMonthsMonthly = sixMonthsMonthly,
                    SixMonthsTotal = sixMonthsTotal,
                    YearMonthly = yearMonthly,
                    YearTotal = yearTotal,
                    Categories = categories,
                };
            return View(model);
            }
        }

        public async Task<IActionResult> LoginPage(LoginViewModel model)
        {

            return View(model);
        }

        public async Task<IActionResult> RegisterPage(RegisterViewModel model)
        {

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model.Login, model.Password);

                if (result)
                {
                    var user = await _userService.GetByLogin(model.Login);
                    userId = user.IdUser;

                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Name, user.Login),
                        //new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
                        //// Додаткові клейми, якщо потрібно
                    };

                    //var claimsIdentity = new ClaimsIdentity(
                    //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    //var authProperties = new AuthenticationProperties
                    //{
                    //    // Додаткові властивості аутентифікації
                    //};

                    //await HttpContext.SignInAsync(
                    //    CookieAuthenticationDefaults.AuthenticationScheme,
                    //    new ClaimsPrincipal(claimsIdentity),
                    //    authProperties);

                    TempData["UserId"] = userId;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View("LoginPage", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Passwords do not match.");
                    return View(model);
                }

                var result = await _userService.RegisterAsync(model.Login, model.Password);
                var user = await _userService.GetByLogin(model.Login);
                userId = user.IdUser;

                if (result)
                {
                    TempData["UserId"] = userId;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed.");
                }
            }

            return View("RegisterPage", model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(LoginPage));
        }
    }
}