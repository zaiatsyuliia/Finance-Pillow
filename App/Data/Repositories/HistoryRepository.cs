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
    Task<List<History>> GetUserHistoryAsync(int userId);
}

public class HistoryRepository : IHistoryRepository
{
    private readonly Context _context;

    public HistoryRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<History>> GetUserHistoryAsync(int userId)
    {
        var userHistory = await _context.Histories
            .Where(h => h.IdUser == userId)
            .OrderByDescending(h => h.Time)
            .ToListAsync();

        return userHistory;
    }
}
