using Mas_Project.Models;
using Mas_Project.Enums;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Services;

public class QuestService
{
    private readonly IQuestRepository _questRepo;
    private readonly IQuestBoardRepository _questBoardRepo;

    public QuestService(IQuestRepository questRepo, IQuestBoardRepository questBoardRepo)
    {
        _questRepo = questRepo;
        _questBoardRepo = questBoardRepo;
    }

    public async Task<Quest?> GetByIdAsync(Guid id)
    {
        return await _questRepo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Quest>> GetAllAsync()
    {
        return await _questRepo.GetAllAsync();
    }

    
    public async Task<Quest> CreateAndAssignQuestAsync(Guid boardId, Guid customerId, Quest quest)
    {
        var board = await _questBoardRepo.GetByIdAsync(boardId);
        if (board.Quests.Any(q => q.Priority == quest.Priority))
            throw new InvalidOperationException("A quest with this priority already exists on the board.");
        quest.QuestBoardId = boardId;
        quest.CustomerId = customerId;
        await _questRepo.AddAsync(quest);
        await _questRepo.SaveChangesAsync();
        return quest;
    }

    public async Task UpdateQuestStatusAsync(Guid questId, QuestStatus newStatus)
    {
        var quest = await _questRepo.GetByIdAsync(questId);
        if (quest == null) throw new ArgumentException("Quest not found.");

        quest.UpdateStatus(newStatus);
        await _questRepo.UpdateAsync(quest);
        await _questRepo.SaveChangesAsync();
    }
}