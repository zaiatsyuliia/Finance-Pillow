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

    public async Task AddTransactionAsync(int userId, TransactionDto dto)
    {
        DateTime currentTime = DateTime.Now;
        if (dto.Type == "expense")
        {
            await _transactionRepository.AddExpenseAsync(userId, dto.CategoryId, currentTime, dto.Sum);
        }
        else { 
            await _transactionRepository.AddIncomeAsync(userId, dto.CategoryId, currentTime, dto.Sum);
        }
    }
}
