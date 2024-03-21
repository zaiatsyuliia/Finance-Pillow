using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IHistoryRepository
{
    Task<List<HistoryEntry>> GetUserHistoryAsync(int userId);
}

public class HistoryRepository : IHistoryRepository
{
    private readonly MyContext _context;

    public HistoryRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<List<HistoryEntry>> GetUserHistoryAsync(int userId)
    {
        var userHistory = await _context.History
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userHistory;
    }
}
