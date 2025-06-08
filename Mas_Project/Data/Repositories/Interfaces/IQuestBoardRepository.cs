using Mas_Project.Models;

namespace Mas_Project.Data.Repositories.Interfaces;

public interface IQuestBoardRepository
{
    Task<QuestBoard?> GetByIdAsync(Guid id);
    Task<IEnumerable<QuestBoard>> GetAllAsync();
    Task AddAsync(QuestBoard board);
    Task UpdateAsync(QuestBoard board);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}