using Business.DTO;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> IndexCopy()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from claims.

        var userHistory = await _budgetService.GetUserExpenseHistoryAsync(userId);

        var model = new CopyViewModel
        {
            History = userHistory,
        };

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddTransaction(TransactionViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transactionDto = new TransactionDto
            {
                Type = model.Type,
                CategoryId = model.CategoryId,
                Sum = model.Sum,
                Details = model.Details,
            };

            await _transactionService.AddTransactionAsync(userId, transactionDto);
        }
        
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteTransaction(TransactionType type, int transactionId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve user ID from claims.

        await _transactionService.DeleteTransactionAsync(type, transactionId);
        TempData["Message"] = "Transaction deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> FilteredHistory(string filterType, DateTime? startDate, DateTime? endDate)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        List<HistoryDto> filteredHistory = new List<HistoryDto>();

        switch (filterType)
        {
            case "expense":
                filteredHistory = await _budgetService.GetUserExpenseHistoryAsync(userId);
                break;
            case "income":
                filteredHistory = await _budgetService.GetUserIncomeHistoryAsync(userId);
                break;
            case "date":
                filteredHistory = await _budgetService.GetUserHistoryTimeAsync(userId, startDate.Value, endDate.Value);
                break;
            default:
                filteredHistory = await _budgetService.GetUserHistoryAsync(userId);
                break;
        }

        return PartialView("_HistoryPartial", filteredHistory);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Expenses()
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Incomes()
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

    [HttpGet]
    public IActionResult Credit()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Currency()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Settings()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var settingsDto = await _settingsService.GetUserSettingsAsync(userId);

        var viewModel = new SettingsViewModel
        {
            UserId = userId,
            Email = settingsDto.Email,
            ExpenseLimit = settingsDto.ExpenseLimit,
            IncomeLimit = settingsDto.IncomeLimit
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Settings(SettingsViewModel model)
    {
        Console.WriteLine("Settings Post Controller!");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userSettingsDto = new SettingsDto
        {
            UserId = userId,
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

        return View(model);
    }

    public async Task<IActionResult> Games()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
