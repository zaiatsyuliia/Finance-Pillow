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
        // Early return for edge cases:
        // If term is zero, or if amount is zero or negative, no valid loan calculation can be done.
        if (term <= 0 || amount <= 0)
        {
            return new CreditDto
            {
                MonthlyPayment = 0,
                TotalPayment = 0,
                TotalInterest = 0
            };
        }

        var monthlyRate = rate / 100 / 12;
        decimal monthlyPayment;

        // Handling the zero rate scenario where the interest does not influence the monthly payments.
        if (rate == 0)
        {
            monthlyPayment = amount / term;
            return new CreditDto
            {
                MonthlyPayment = Math.Round(monthlyPayment, 2),
                TotalPayment = Math.Round(amount, 2),
                TotalInterest = 0
            };
        }

        // Calculate the denominator for the credit formula to avoid division by zero issues.
        var denominator = Math.Pow(1 + (double)monthlyRate, -term);
        if (Math.Abs(1 - denominator) < 0.000001)
        {
            return new CreditDto
            {
                MonthlyPayment = 0,
                TotalPayment = amount,
                TotalInterest = 0
            };
        }

        monthlyPayment = (amount * monthlyRate) / (decimal)(1 - denominator);
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
