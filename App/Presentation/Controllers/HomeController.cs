using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Business.Service;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Business.DTO;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly BudgetService _budgetService;
        private readonly TransactionService _transactionService;
        private readonly CategoryService _categoryService;
        private readonly StatisticsService _statisticsService;

        public HomeController(BudgetService budgetService,
               TransactionService transactionService,
               CategoryService categoryService,
               StatisticsService statisticsService)
        {
            _budgetService = budgetService;
            _transactionService = transactionService;
            _categoryService = categoryService;
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = 1; // Replace with actual user ID
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
            var userId = 1;

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
            var userId = 1; // Replace with actual user ID

            var monthDaily = await _statisticsService.GetExpenseMonthDailyAsync(userId);
            var monthTotal = await _statisticsService.GetExpenseMonthTotalAsync(userId);
            var sixMonthsMonthly = await _statisticsService.GetExpense6MonthsMonthlyAsync(userId);
            var sixMonthsTotal = await _statisticsService.GetExpense6MonthsTotalAsync(userId);
            var yearMonthly = await _statisticsService.GetExpenseYearMonthlyAsync(userId);
            var yearTotal = await _statisticsService.GetExpenseYearTotalAsync(userId);

            var model = new StatisticsViewModel
            {
                MonthDaily = monthDaily,
                MonthTotal = monthTotal,
                SixMonthsMonthly = sixMonthsMonthly,
                SixMonthsTotal = sixMonthsTotal,
                YearMonthly = yearMonthly,
                YearTotal = yearTotal
            };

            return View(model);
        }

        public async Task<IActionResult> Incomes()
        {
            var userId = 1; // Replace with actual user ID

            var monthDaily = await _statisticsService.GetIncomeMonthDailyAsync(userId);
            var monthTotal = await _statisticsService.GetIncomeMonthTotalAsync(userId);
            var sixMonthsMonthly = await _statisticsService.GetIncome6MonthsMonthlyAsync(userId);
            var sixMonthsTotal = await _statisticsService.GetIncome6MonthsTotalAsync(userId);
            var yearMonthly = await _statisticsService.GetIncomeYearMonthlyAsync(userId);
            var yearTotal = await _statisticsService.GetIncomeYearTotalAsync(userId);

            var model = new StatisticsViewModel
            {
                MonthDaily = monthDaily,
                MonthTotal = monthTotal,
                SixMonthsMonthly = sixMonthsMonthly,
                SixMonthsTotal = sixMonthsTotal,
                YearMonthly = yearMonthly,
                YearTotal = yearTotal
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
