using Mas_Project.Models;

namespace Mas_Project.Data.Repositories;


public interface IGuildMemberRepository
{
    Task<GuildMember?> GetByIdAsync(Guid id);
    Task<IEnumerable<GuildMember>> GetAllAsync();
    Task AddAsync(GuildMember member);
    Task UpdateAsync(GuildMember member);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}