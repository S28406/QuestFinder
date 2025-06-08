using Mas_Project.Models;

namespace Mas_Project.Data.Repositories;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(Guid id);
    Task<IEnumerable<Team>> GetAllAsync();
    Task AddAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}