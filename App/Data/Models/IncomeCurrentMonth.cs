using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeCurrentMonth
{
    public int? IdUser { get; set; }

    public int? IdCategoryIncome { get; set; }

    public double? TotalSum { get; set; }
}
