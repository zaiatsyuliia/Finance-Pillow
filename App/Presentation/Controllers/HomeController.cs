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

        public async Task<IActionResult> Index(int userId)
        {
            Console.WriteLine(userId);
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

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionViewModel model)
        {
            if (ModelState.IsValid)
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

        public async Task<IActionResult> LoginPage(LoginViewModel model)
        { 

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine(model.Login, model.Password);
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                // Call the UserService to attempt login
                var result = await _userService.LoginAsync(model.Login, model.Password);
                
                Console.WriteLine(result);

                if (result)
                {
                    // Login successful, redirect to the home page
                    var user = await _userService.GetByLogin(model.Login);
                    Console.WriteLine(user.IdUser);
                    userId = user.IdUser;
                    Console.WriteLine(userId);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Login failed, add an error message to the ModelState
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If login failed, return the login view with validation errors
            return View("LoginPage", model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(LoginRegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Call the UserService to attempt registration
        //        var result = await _userService.RegisterAsync(model.RegisterLogin, model.RegisterPassword);

        //        if (result)
        //        {
        //            var user = await _userService.GetByLogin(model.RegisterLogin);
        //            userId = user.IdUser;

        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            // Registration failed, add an error message to the ModelState
        //            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
        //        }
        //    }

        //    return View("LoginPage", model);
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Call ASP.NET Core's built-in sign out functionality
            userId = 0; // magic number

            // Redirect the user to the login page or any other page
            return RedirectToAction("LoginPage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
