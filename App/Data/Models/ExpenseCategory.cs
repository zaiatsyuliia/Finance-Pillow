using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ExpenseCategory
{
    public int ExpenseCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
