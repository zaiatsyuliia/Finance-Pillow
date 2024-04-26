using Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class ApplicationUser : IdentityUser
{
    //// Collection navigation property for Expenses
    //public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

    //// Collection navigation property for Incomes
    //public virtual ICollection<Income> Incomes { get; set; } = new HashSet<Income>();

    //// Reference navigation property for UserSettings
    //public virtual UserSetting UserSetting { get; set; }
}
