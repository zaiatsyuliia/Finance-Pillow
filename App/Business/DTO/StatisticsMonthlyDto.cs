using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO;

public class StatisticsMonthlyDto
{
    public DateTime Month { get; set; }
    public string CategoryName { get; set; }
    public double TotalSum { get; set; }
}
