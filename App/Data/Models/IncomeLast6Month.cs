using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class IncomeLast6Month
{
    public int? IdUser { get; set; }

    public int? IdCategoryIncome { get; set; }

    public DateTime? Month { get; set; }

    public double? TotalSum { get; set; }
}
