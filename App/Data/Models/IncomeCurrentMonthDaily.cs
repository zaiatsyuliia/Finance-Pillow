using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeCurrentMonthDaily
{
    public int? IdUser { get; set; }

    public int? IdCategoryIncome { get; set; }

    public DateTime? IncomeDate { get; set; }

    public double? TotalSum { get; set; }
}
