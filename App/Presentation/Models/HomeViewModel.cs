using Data.Models;

namespace Presentation.Models;

public class HomeViewModel
{
    public string UserBudget { get; set; }

    public List<History> UserHistory { get; set; }

    public List<ExpenseCategory> ExpenseCategories { get; set; }

    public List<IncomeCategory> IncomeCategories { get; set; }
}

public class TransactionViewModel
{
    public string Type { get; set; }

    public int CategoryId { get; set; }

    public double Sum { get; set; }
}