using Data.Models;
using Business.DTO;

namespace Presentation.Models;

public class HomeViewModel
{
    public double Budget { get; set; }

    public List<HistoryDto> History { get; set; }

    public List<CategoryDto> ExpenseCategories { get; set; }

    public List<CategoryDto> IncomeCategories { get; set; }

    public LimitDTO ExpenseLimit { get; set; }

    public LimitDTO IncomeLimit { get; set; }

}

public class TransactionViewModel
{
    public string Type { get; set; }

    public int CategoryId { get; set; }

    public int Sum { get; set; }
}