using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IHistoryRepository
{
    Task<List<History>> GetUserHistoryAsync(string userId);

}

public class HistoryRepository : IHistoryRepository
{
    private readonly FPDbContext _context;

    public HistoryRepository(FPDbContext context)
    {
        _context = context;
    }

    public async Task<List<History>> GetUserHistoryAsync(string userId)
    {
        // Assuming History maps to a view or table that amalgamates user transactions
        var userHistory = await _context.Histories
            .Where(h => h.UserId == userId) // Ensuring the correct property is used for filtering by user ID
            .OrderByDescending(h => h.Time) // Assuming 'Time' is the timestamp of the history entry
            .ToListAsync();

        return userHistory;
    }
    
}
