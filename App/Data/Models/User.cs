using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public int ExpenseLimit { get; set; } = int.MaxValue;

    public int IncomeLimit { get; set; } = int.MaxValue;
}
