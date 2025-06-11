using Mas_Project.Data.DTOs;
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

    // public async Task<QuestDTO?> GetByIdAsync(Guid id)
    // {
    //     return await _context.Quests
    //         .Where(q => q.QuestID == id)
    //         .Select(q => new QuestDTO
    //         {
    //             QuestID = q.QuestID,
    //             Title = q.Title,
    //             Description = q.Description,
    //             Reward = q.Reward,
    //             MinRank = q.MinRank,
    //             DurationHours = q.DurationHours,
    //             Requirements = q.Requirements,
    //             Participants = q.DateTakens
    //                 .Where(dt => dt.Date.AddHours(q.DurationHours) > DateTime.Now)
    //                 .Select(dt => new ParticipantDTO
    //                 {
    //                     UserID = dt.GuildMember.UserID,
    //                     Username = dt.GuildMember.Username
    //                 })
    //                 .ToList()
    //         })
    //         .FirstOrDefaultAsync();
    // }
    
    public async Task<QuestDTO?> GetByIdAsync(Guid id)
    {
        var quest = await _context.Quests
            .Where(q => q.QuestID == id)
            .Select(q => new
            {
                q.QuestID,
                q.Title,
                q.Description,
                q.Reward,
                q.MinRank,
                q.DurationHours,
                q.Requirements,
                Participants = q.DateTakens.Select(dt => new
                {
                    dt.Date,
                    dt.GuildMember.UserID,
                    dt.GuildMember.Username
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (quest == null)
            return null;

        var activeParticipants = quest.Participants
            .Where(p => p.Date.AddHours(quest.DurationHours) > DateTime.UtcNow)
            .Select(p => new ParticipantDTO
            {
                UserID = p.UserID,
                Username = p.Username
            })
            .ToList();

        return new QuestDTO
        {
            QuestID = quest.QuestID,
            Title = quest.Title,
            Description = quest.Description,
            Reward = quest.Reward,
            MinRank = quest.MinRank,
            DurationHours = quest.DurationHours,
            Requirements = quest.Requirements,
            Participants = activeParticipants
        };
    }

    public Quest? GetQuestByIdAsync(Guid id)
    {
        var quest = _context.Quests.FirstOrDefault(q => q.QuestID == id);
        return quest;
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
        var quest = GetQuestByIdAsync(id);
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