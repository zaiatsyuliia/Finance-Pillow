using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeCategory
{
    public int IdCategoryIncome { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
}
