using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class History
{
    public string? TransactionType { get; set; }

    public int? TransactionId { get; set; }

    public string? UserId { get; set; }

    public DateTime? Time { get; set; }

    public string? Category { get; set; }

    public double? Sum { get; set; }
}
