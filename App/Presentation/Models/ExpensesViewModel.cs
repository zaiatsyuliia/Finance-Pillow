using Data.Models;

namespace Presentation.Models;
public class ExpensesViewModel
{
	public List<ExpenseMonthDaily> MonthDaily { get; set; }

	public List<ExpenseMonthTotal> MonthTotal { get; set; }

	public List<Expense6MonthsMonthly> SixMonthsMonthly { get; set; }

	public List<Expense6MonthsTotal> SixMonthsTotal { get; set; }

	public List<ExpenseYearMonthly> YearMonthly { get; set; }

	public List<ExpenseYearTotal> YearTotal { get; set; }
}
