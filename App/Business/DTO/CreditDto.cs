using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class CreditDto
    {
        public decimal MonthlyPayment { get; set; } // Щомісячний платіж
        public decimal TotalPayment { get; set; } // Загальна сума виплати
        public decimal TotalInterest { get; set; } // Відсотки
    }
}
