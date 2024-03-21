using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeMonthTotal
{
    public int? IdUser { get; set; }

    public string? CategoryName { get; set; }

    public double? TotalSum { get; set; }
}
