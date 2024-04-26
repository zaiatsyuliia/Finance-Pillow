using Business.DTO;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Presentation.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Presentation.Controllers;

public class HomeController : Controller
{

    private readonly BudgetService _budgetService;
    private readonly TransactionService _transactionService;
    private readonly CategoryService _categoryService;
    private readonly StatisticsService _statisticsService;
    private readonly SettingsService _settingsService;

    public HomeController(BudgetService budgetService, TransactionService transactionService, CategoryService categoryService, StatisticsService statisticsService, SettingsService settingsService)
    {
        _budgetService = budgetService;
        _transactionService = transactionService;
        _categoryService = categoryService;
        _statisticsService = statisticsService;
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated) // This assumes you are using ASP.NET Core Identity for user authentication.
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from claims.

            var userBudget = await _budgetService.GetUserBudgetAsync(userId);
            var userHistory = await _budgetService.GetUserHistoryAsync(userId);
            var expenseCategories = await _categoryService.GetExpenseCategoriesAsync();
            var incomeCategories = await _categoryService.GetIncomeCategoriesAsync();
            var limits = await _budgetService.GetLimitsAsync(userId);

            var model = new BudgetViewModel
            {
                Budget = userBudget,
                History = userHistory,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories,
                Limits = limits,
            };

            return View(model);
        }
        else
        {
            return RedirectToAction("Login", "Account"); // Redirect to the AccountController's Login action if not authenticated.
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction(TransactionViewModel model)
    {
        if (ModelState.IsValid && User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transactionDto = new TransactionDto
            {
                Type = model.Type,
                CategoryId = model.CategoryId,
                Sum = model.Sum
            };

            await _transactionService.AddTransactionAsync(userId, transactionDto);

            return RedirectToAction("Index");
        }

        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTransaction(TransactionType type, int transactionId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve user ID from claims.

            await _transactionService.DeleteTransactionAsync(type, transactionId);
            TempData["Message"] = "Transaction deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["Error"] = "Unable to delete transaction. User not recognized.";
            return RedirectToAction(nameof(Login), "Account");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Expenses()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
            return RedirectToAction("Login", "Account");  // Redirect to the login action in the AccountController.
        }
    }

    [HttpGet]
    public async Task<IActionResult> Incomes()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
            return RedirectToAction("Login", "Account");  // Redirect to the login action in the AccountController.
        }
    }

    [HttpGet]
    public IActionResult Credit()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Currency()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Settings()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSettingsDto = await _settingsService.GetUserSettingsAsync(userId);
            if (userSettingsDto != null)
            {
                var viewModel = new SettingsViewModel
                {
                    UserId = userId,
                    UserName = userSettingsDto.UserName,
                    Email = userSettingsDto.Email,
                    ExpenseLimit = userSettingsDto.ExpenseLimit,
                    IncomeLimit = userSettingsDto.IncomeLimit
                };
                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "User settings not found.";
            }
        }
        return RedirectToAction("Login", "Account");
    }

    public async Task<IActionResult> BullsAndCows()
    {
        return View();
    }

    public async Task<IActionResult> Hangman()
    {
        return View();
    }
    public async Task<IActionResult> TicTacToe()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Settings(SettingsViewModel model)
    {
        if (User.Identity.IsAuthenticated)
        {
            Console.WriteLine("Settings Post Controller!");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.Equals(model.NewPassword, model.ConfirmPassword))
            {
                ModelState.AddModelError("", "The new password and confirmation password do not match.");
                return View(model);
            }

            var userSettingsDto = new SettingsDto
            {
                UserId = userId,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.NewPassword,
                ConfirmPassword = model.ConfirmPassword,
                ExpenseLimit = model.ExpenseLimit,
                IncomeLimit = model.IncomeLimit
            };

            try
            {
                await _settingsService.UpdateSettingsAsync(userSettingsDto);
                TempData["SuccessMessage"] = "Settings updated successfully.";
                return RedirectToAction(nameof(Settings));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to update settings: " + ex.Message);
            }
        }
        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
