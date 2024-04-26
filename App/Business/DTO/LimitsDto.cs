namespace Business.DTO;

public class LimitsDto
{
    public string UserId { get; set; }

    public double TotalExpense { get; set; }
    public int ExpenseLimit { get; set; }
    public bool ExpenseLimitExceeded { get; set; }

    public double TotalIncome { get; set; }
    public int IncomeLimit { get; set; }
    public bool IncomeLimitExceeded { get; set; }
}
