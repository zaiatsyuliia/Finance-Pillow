using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public string? UserId { get; set; }

    public int? IncomeCategoryId { get; set; }

    public DateTime? Time { get; set; }

    public double Sum { get; set; }

    public string? Details { get; set; }

    public virtual IncomeCategory? IncomeCategory { get; set; }

    public virtual ApplicationUser User { get; set; }
}
