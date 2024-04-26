using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public partial class UserSetting
{
    public string? UserId { get; set; }

    public int? ExpenseLimit { get; set; }

    public int? IncomeLimit { get; set; }

    public virtual ApplicationUser User { get; set; }
}
