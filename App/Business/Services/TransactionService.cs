using System;
using System.Threading.Tasks;
using System.Transactions;
using Data.Models;
using Data.Repositories;
using Business.DTO;

namespace Business.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task AddTransactionAsync(string userId, TransactionDto dto)
    {
        DateTime currentTime = DateTime.Now;
        switch (dto.Type)
        {
            case TransactionType.Expense:
                await _transactionRepository.AddExpenseAsync(userId, dto.CategoryId, currentTime, dto.Sum);
                break;
            case TransactionType.Income:
                await _transactionRepository.AddIncomeAsync(userId, dto.CategoryId, currentTime, dto.Sum);
                break;
            default:
                throw new ArgumentException("Invalid transaction type specified.");
        }
    }
    public async Task DeleteTransactionAsync(TransactionType type, int transactionId)
    {
        switch (type)
        {
            case TransactionType.Income:
                await _transactionRepository.DeleteIncomeAsync(transactionId);
                break;
            case TransactionType.Expense:
                await _transactionRepository.DeleteExpenseAsync(transactionId);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid transaction type specified.");
        }
    }

}
