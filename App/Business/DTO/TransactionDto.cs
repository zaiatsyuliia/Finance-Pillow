namespace Business.DTO;

public enum TransactionType
{
    Expense,
    Income
}

public class TransactionDto
{
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
    public double Sum { get; set; }

    public string Details { get; set; }
}