using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO;

public class HistoryDto
{
    public string TransactionType { get; set; }
    public int IdTransaction { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; } // Representing only date
    public double Sum { get; set; }
}
