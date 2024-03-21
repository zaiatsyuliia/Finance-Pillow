using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class HistoryEntry
{
    public string TransactionType { get; set; }
    public int UserId { get; set; }
    public string Category { get; set; }
    public DateTime Time { get; set; }
    public decimal Sum { get; set; }
}
