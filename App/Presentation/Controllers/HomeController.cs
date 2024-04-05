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
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly BudgetService _budgetService;
    private readonly TransactionService _transactionService;
    private readonly CategoryService _categoryService;
    private readonly StatisticsService _statisticsService;
    private readonly UserService _userService;
    private readonly CreditService _creditService;

    public HomeController(BudgetService budgetService,
           TransactionService transactionService,
           CategoryService categoryService,
           StatisticsService statisticsService,
           UserService userService,
           CreditService creditService)
    {
        _budgetService = budgetService;
        _transactionService = transactionService;
        _categoryService = categoryService;
        _statisticsService = statisticsService;
        _userService = userService;
        _creditService = creditService;
    }

    public async Task<IActionResult> Index()
    {
        if (Request.Cookies.TryGetValue("UserId", out string userIdString) && int.TryParse(userIdString, out int userId))
        {
            Console.WriteLine($"Logged in user ID: {userId}");

            var userBudget = await _budgetService.GetUserBudgetAsync(userId);
            var userHistory = await _budgetService.GetUserHistoryAsync(userId);
            var expenseCategories = await _categoryService.GetExpenseCategoriesAsync();
            var incomeCategories = await _categoryService.GetIncomeCategoriesAsync();
            var limit = await _budgetService.GetLimitAsync(userId);

            var model = new HomeViewModel
            {
                Budget = userBudget,
                History = userHistory,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories,
                Limit = limit // Add the LimitDTO to the model
            };

            return View(model);
        }
        else
        {
            // Redirect to login page if userId cookie doesn't exist or cannot be parsed
            return RedirectToAction(nameof(LoginPage));
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddTransaction(TransactionViewModel model)
    {
        if (ModelState.IsValid && Request.Cookies.TryGetValue("UserId", out string userIdString) && int.TryParse(userIdString, out int userId))
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
        if (Request.Cookies.TryGetValue("UserId", out string userIdString) && int.TryParse(userIdString, out int userId))
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
        else
        {
            // Redirect to login page if userId cookie doesn't exist or cannot be parsed
            return RedirectToAction(nameof(LoginPage));
        }
    }

    public async Task<IActionResult> Incomes()
    {
        if (Request.Cookies.TryGetValue("UserId", out string userIdString) && int.TryParse(userIdString, out int userId))
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
        else
        {
            // Redirect to login page if userId cookie doesn't exist or cannot be parsed
            return RedirectToAction(nameof(LoginPage));
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
                var userId = user.IdUser;

                // Create a cookie containing the userId
                var cookieOptions = new CookieOptions
                {
                    // Set other options if needed, like expiration time, domain, etc.
                    // Expires = DateTime.UtcNow.AddHours(1),
                    // HttpOnly = true,
                    // Secure = true,
                    // SameSite = SameSiteMode.Strict
                };

                // Save userId in the cookie
                Response.Cookies.Append("UserId", userId.ToString(), cookieOptions);

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

            if (result && user != null)
            {
                var userId = user.IdUser;

                // Create a cookie containing the userId
                var cookieOptions = new CookieOptions
                {
                    // Set other options if needed
                    // Expires = DateTime.UtcNow.AddHours(1),
                    // HttpOnly = true,
                    // Secure = true,
                    // SameSite = SameSiteMode.Strict
                };

                // Save userId in the cookie
                Response.Cookies.Append("UserId", userId.ToString(), cookieOptions);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }
        }

        return View("RegisterPage", model);
    }

    public async Task<IActionResult> CalculateCredit(int term, decimal amount, decimal rate)
    {
        var creditDto = _creditService.CalculateCredit(term, amount, rate);

        return Json(new
        {
            monthlyPayment = creditDto.MonthlyPayment,
            totalPayment = creditDto.TotalPayment,
            totalInterest = creditDto.TotalInterest
        });
    }

    public async Task<IActionResult> CreditPage(CreditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var creditDto = _creditService.CalculateCredit(model.Term, model.Amount, model.Rate);

            model.MonthlyPayment = creditDto.MonthlyPayment;
            model.TotalPayment = creditDto.TotalPayment;
            model.TotalInterest = creditDto.TotalInterest;

            return View(model);
        }

        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        // Clear the userId cookie
        Response.Cookies.Delete("UserId");

        // Sign out the user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(LoginPage));
    }
}