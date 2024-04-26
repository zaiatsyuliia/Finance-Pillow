using Business.DTO;

namespace Presentation.Models;

public class TransactionViewModel
{
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
    public int Sum { get; set; }
}