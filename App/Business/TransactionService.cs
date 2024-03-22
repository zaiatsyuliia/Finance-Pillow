using System;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace Business.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task AddExpenseAsync(int userId, int categoryId, double sum)
    {
        DateTime currentTime = DateTime.Now;

        await _transactionRepository.AddExpenseAsync(userId, categoryId, currentTime, sum);
    }

    public async Task AddIncomeAsync(int userId, int categoryId, double sum)
    {
        DateTime currentTime = DateTime.Now;

        await _transactionRepository.AddIncomeAsync(userId, categoryId, currentTime, sum);
    }
}
