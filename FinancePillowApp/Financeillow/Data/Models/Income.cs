namespace Financeillow.Data.Models;

public partial class Income
{
    public int IdIncome { get; set; }

    public int? IdUser { get; set; }

    public int? IdCategoryIncome { get; set; }

    public DateTime? Time { get; set; }

    public double Sum { get; set; }

    public virtual IncomeCategory? IdCategoryIncomeNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
