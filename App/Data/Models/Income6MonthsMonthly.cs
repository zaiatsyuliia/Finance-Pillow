using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Income6MonthsMonthly
{
    public string? UserId { get; set; }

    public DateTime? Month { get; set; }

    public string? CategoryName { get; set; }

    public double? TotalSum { get; set; }
}
