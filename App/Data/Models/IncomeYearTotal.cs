using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeYearTotal
{
    public string? UserId { get; set; }

    public string? CategoryName { get; set; }

    public double? TotalSum { get; set; }
}
