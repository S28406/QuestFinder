using Microsoft.EntityFrameworkCore;
using Mas_Project.Models;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Data.Repositories;

public class QuestBoardRepository : IQuestBoardRepository
{
    private readonly DBContext _context;

    public QuestBoardRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<QuestBoard?> GetByIdAsync(Guid id)
    {
        return await _context.QuestBoards
            .Include(qb => qb.Quests)
            .FirstOrDefaultAsync(qb => qb.QuestBoardID == id);
    }

    public async Task<IEnumerable<QuestBoard>> GetAllAsync()
    {
        return await _context.QuestBoards.Include(qb => qb.Quests).ToListAsync();
    }

    public async Task AddAsync(QuestBoard board)
    {
        await _context.QuestBoards.AddAsync(board);
    }

    public async Task UpdateAsync(QuestBoard board)
    {
        _context.QuestBoards.Update(board);
    }

    public async Task DeleteAsync(Guid id)
    {
        var board = await GetByIdAsync(id);
        if (board != null)
        {
            _context.QuestBoards.Remove(board);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}