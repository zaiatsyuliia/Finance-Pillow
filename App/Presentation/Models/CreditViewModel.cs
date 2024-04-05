using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models
{
    public class CreditViewModel
    {
        public int Term { get; set; } // Термін кредитування (місяці)
        public decimal Amount { get; set; } // Сума кредиту (грн)
        public decimal Rate { get; set; } // Відсоткова ставка (% річних)
        public decimal MonthlyPayment { get; set; } // Щомісячний платіж
        public decimal TotalPayment { get; set; } // Загальна сума виплати
        public decimal TotalInterest { get; set; } // Відсотки
    }
}
