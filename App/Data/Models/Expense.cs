using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public string? UserId { get; set; }

    public int? ExpenseCategoryId { get; set; }

    public DateTime? Time { get; set; }

    public double Sum { get; set; }

    public string? Details { get; set; }

    public virtual ExpenseCategory? ExpenseCategory { get; set; }

    public virtual ApplicationUser User { get; set; }
}
