using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ExpenseCurrentMonthDaily
{
    public int? IdUser { get; set; }

    public int? IdCategoryExpense { get; set; }

    public DateTime? ExpenseDate { get; set; }

    public double? TotalSum { get; set; }
}
