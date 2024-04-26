using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Limit
{
    public string? UserId { get; set; }

    public double? TotalExpense { get; set; }

    public int? ExpenseLimit { get; set; }

    public double? TotalIncome { get; set; }

    public int? IncomeLimit { get; set; }

    public bool? ExpenseLimitExceeded { get; set; }

    public bool? IncomeLimitExceeded { get; set; }
}
