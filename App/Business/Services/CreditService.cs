using Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class CreditService
{
    public CreditDto CalculateCredit(int term, decimal amount, decimal rate)
    {
        var monthlyRate = rate / 100 / 12;

        // Перевірка на ділення на нуль
        if (Math.Abs(1 - Math.Pow(1 + (double)monthlyRate, -term)) < 0.000001)
        {
            return new CreditDto
            {
                MonthlyPayment = 0,
                TotalPayment = amount,
                TotalInterest = 0
            };
        }

        var monthlyPayment = (amount * monthlyRate) / (decimal)(1 - Math.Pow(1 + (double)monthlyRate, -term));

        var totalPayment = monthlyPayment * term;
        var totalInterest = totalPayment - amount;

        return new CreditDto
        {
            MonthlyPayment = Math.Round(monthlyPayment, 2),
            TotalPayment = Math.Round(totalPayment, 2),
            TotalInterest = Math.Round(totalInterest, 2)
        };
    }

}
