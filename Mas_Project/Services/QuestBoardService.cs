using Mas_Project.Models;
using Mas_Project.Data.Repositories;
using Mas_Project.Data.Repositories.Interfaces;

namespace Mas_Project.Services;

public class QuestBoardService
{
    private readonly IQuestBoardRepository _questBoardRepo;

    public QuestBoardService(IQuestBoardRepository questBoardRepo)
    {
        _questBoardRepo = questBoardRepo;
    }

    public async Task<List<Quest>> GetAllQuestsAsync(Guid questBoardId)
    {
        var board = await _questBoardRepo.GetByIdAsync(questBoardId);
        return (List<Quest>)board?.Quests ?? new List<Quest>();
    }

    public async Task<List<Quest>> FilterQuestsByRankAsync(Guid questBoardId, int rank)
    {
        var board = await _questBoardRepo.GetByIdAsync(questBoardId);
        return board?.Quests.Where(q => q.MinRank == rank).ToList() ?? new List<Quest>();
    }

    public async Task AddQuestToBoardAsync(Guid questBoardId, Quest quest)
    {
        var board = await _questBoardRepo.GetByIdAsync(questBoardId);
        if (board == null)
            throw new ArgumentException("Quest board not found");

        // Check uniqueness of priority
        if (board.Quests.Any(q => q.Priority == quest.Priority))
            throw new InvalidOperationException("A quest with this priority already exists on the board.");

        // Set FK relationship
        quest.QuestBoardId = questBoardId;

        board.Quests.Add(quest);
        await _questBoardRepo.UpdateAsync(board);
        await _questBoardRepo.SaveChangesAsync();
    }

}