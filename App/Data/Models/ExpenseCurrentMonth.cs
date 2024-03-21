using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ExpenseCurrentMonth
{
    public int? IdUser { get; set; }

    public int? IdCategoryExpense { get; set; }

    public double? TotalSum { get; set; }
}
