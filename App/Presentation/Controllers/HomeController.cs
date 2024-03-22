using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Business;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly BudgetService _budgetService;
        private readonly TransactionService _transactionService;
        private readonly CategoryService _categoryService;

        public HomeController(BudgetService budgetService, TransactionService transactionService, CategoryService categoryService)
        {
            _budgetService = budgetService;
            _transactionService = transactionService;
            _categoryService = categoryService;
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
                UserBudget = userBudget?.ToString("C") ?? "Budget not available",
                UserHistory = userHistory,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionViewModel model)
        {
            var userId = 1;
            // Log model values to the console
            Console.WriteLine($"UserId: {userId}");
            Console.WriteLine($"Type: {model.Type}");
            Console.WriteLine($"CategoryId: {model.CategoryId}");
            Console.WriteLine($"Sum: {model.Sum}");

            if (ModelState.IsValid)
            {
                if (model.Type == "Expense")
                {
                    await _transactionService.AddExpenseAsync(userId, model.CategoryId, model.Sum);
                }
                else if (model.Type == "Income")
                {
                    await _transactionService.AddIncomeAsync(userId, model.CategoryId, model.Sum);
                }
                else
                {
                    return BadRequest("Invalid transaction type.");
                }

                return RedirectToAction(nameof(Index));
            }

            return View("Index", model);
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