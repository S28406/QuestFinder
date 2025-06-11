using System.IO;
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

    public async Task<List<QuestBoard>> GetAllBoardsAsync()
    {
        var boards = await _questBoardRepo.GetAllAsync();
        Console.WriteLine($"Boards found: {boards.Count()}, From service");

        var random = new Random();
        string imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        string[] availableImages = Array.Empty<string>();

        if (Directory.Exists(imageDir))
        {
            availableImages = Directory.GetFiles(imageDir, "*.*")
                .Where(f => f.EndsWith(".png"))
                .Select(Path.GetFileName)
                .ToArray();
        }

        foreach (var board in boards)
        {
            if (string.IsNullOrWhiteSpace(board.ImageUrl) && availableImages.Length > 0)
            {
                var randomImage = availableImages[random.Next(availableImages.Length)];
                board.ImageUrl = Path.Combine("Images", randomImage).Replace("\\", "/");
            }
        }

        return (List<QuestBoard>)boards;
    }

    public async Task<List<Quest>> FilterQuestsByRankAsync(Guid questBoardId, int rank)
    {
        var board = await _questBoardRepo.GetByIdAsync(questBoardId);
        return board?.Quests.Where(q => q.MinRank == rank).ToList() ?? new List<Quest>();
    }

    public async Task<QuestBoard> AddBoardAsync(string location, string name)
    {
        if (_questBoardRepo.GetAllAsync().Result.Any(qb => qb.Location == location))
        {
            throw new Exception($"There is already an existing board in the location {location}");
        }
        var board = new QuestBoard(Guid.NewGuid(), location, name);
        await _questBoardRepo.AddAsync(board);
        await _questBoardRepo.SaveChangesAsync();
        return board;
    }
}