using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ExpenseLast6Month
{
    public int? IdUser { get; set; }

    public int? IdCategoryExpense { get; set; }

    public DateTime? Month { get; set; }

    public double? TotalSum { get; set; }
}
