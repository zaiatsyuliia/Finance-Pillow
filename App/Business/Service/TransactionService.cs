using System;
using System.Threading.Tasks;
using System.Transactions;
using Data.Models;
using Data.Repositories;
using Business.DTO;

namespace Business.Service;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task AddExpenseAsync(int userId, TransactionDto dto)
    {
        DateTime currentTime = DateTime.Now;

        await _transactionRepository.AddExpenseAsync(userId, dto.CategoryId, currentTime, dto.Sum);
    }

    public async Task AddIncomeAsync(int userId, TransactionDto dto)
    {
        DateTime currentTime = DateTime.Now;

        await _transactionRepository.AddIncomeAsync(userId, dto.CategoryId, currentTime, dto.Sum);
    }
}
