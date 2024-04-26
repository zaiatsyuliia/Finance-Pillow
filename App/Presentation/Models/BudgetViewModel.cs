using Data.Models;
using Business.DTO;

namespace Presentation.Models;

public class BudgetViewModel
{
    public double Budget { get; set; }

    public List<HistoryDto> History { get; set; }

    public List<CategoryDto> ExpenseCategories { get; set; }

    public List<CategoryDto> IncomeCategories { get; set; }

    public LimitsDto Limits { get; set; }

}
