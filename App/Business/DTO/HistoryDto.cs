namespace Business.DTO;

public class HistoryDto
{
    public string TransactionType { get; set; }
    public int TransactionId { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public double Sum { get; set; }
}
