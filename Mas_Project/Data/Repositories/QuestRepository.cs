using Microsoft.EntityFrameworkCore;
using Mas_Project.Models;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Data.Repositories;

public class QuestRepository : IQuestRepository
{
    private readonly DBContext _context;

    public QuestRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<Quest?> GetByIdAsync(Guid id)
    {
        return await _context.Quests
            .Include(q => q.DateTakens)
            .ThenInclude(dt => dt.GuildMember)
            .FirstOrDefaultAsync(q => q.QuestID == id);
    }

    public async Task<IEnumerable<Quest>> GetAllAsync()
    {
        return await _context.Quests.ToListAsync();
    }

    public async Task AddAsync(Quest quest)
    {
        await _context.Quests.AddAsync(quest);
    }

    public async Task UpdateAsync(Quest quest)
    {
        _context.Quests.Update(quest);
    }

    public async Task DeleteAsync(Guid id)
    {
        var quest = await GetByIdAsync(id);
        if (quest != null)
        {
            _context.Quests.Remove(quest);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}