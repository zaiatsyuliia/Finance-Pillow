namespace Financeillow.Data.Models;

public partial class Expense
{
    public int IdExpense { get; set; }

    public int? IdUser { get; set; }

    public int? IdCategoryExpense { get; set; }

    public DateTime? Time { get; set; }

    public double Sum { get; set; }

    public virtual ExpenseCategory? IdCategoryExpenseNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
