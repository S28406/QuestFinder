using Mas_Project.Data.DTOs;
using Mas_Project.Models;

namespace Mas_Project.Data.Repositories.Interfaces;

public interface IQuestRepository
{
    Task<QuestDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<Quest>> GetAllAsync();
    Task AddAsync(Quest quest);
    Task UpdateAsync(Quest quest);
    
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
    Quest? GetQuestByIdAsync(Guid id);
}