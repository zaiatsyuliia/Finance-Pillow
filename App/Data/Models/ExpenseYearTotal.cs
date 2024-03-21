using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ExpenseYearTotal
{
    public int? IdUser { get; set; }

    public string? CategoryName { get; set; }

    public double? TotalSum { get; set; }
}
