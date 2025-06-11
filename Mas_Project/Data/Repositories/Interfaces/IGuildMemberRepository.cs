using Mas_Project.Data.DTOs;
using Mas_Project.Models;

namespace Mas_Project.Data.Repositories;


public interface IGuildMemberRepository
{
    Task<GuildMember?> GetByIdAsync(Guid id);
    public Task<IEnumerable<GuildMemberDTO>> GetAllIdsAsync();
    Task<IEnumerable<GuildMember>> GetAllAsync();
    Task AddAsync(GuildMember member);
    Task UpdateAsync(GuildMember member);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}