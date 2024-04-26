using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeMonthDaily
{
    public string? UserId { get; set; }

    public DateTime? Day { get; set; }

    public string? CategoryName { get; set; }

    public double? TotalSum { get; set; }
}
