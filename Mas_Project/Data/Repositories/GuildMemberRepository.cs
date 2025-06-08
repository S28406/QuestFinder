using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data.Repositories;

public class GuildMemberRepository : IGuildMemberRepository
{
    private readonly DBContext _context;

    public GuildMemberRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<GuildMember?> GetByIdAsync(Guid id)
    {
        return await _context.GuildMembers.FindAsync(id);
    }

    public async Task<IEnumerable<GuildMember>> GetAllAsync()
    {
        return await _context.GuildMembers.ToListAsync();
    }

    public async Task AddAsync(GuildMember member)
    {
        await _context.GuildMembers.AddAsync(member);
    }

    public async Task UpdateAsync(GuildMember member)
    {
        _context.GuildMembers.Update(member);
    }

    public async Task DeleteAsync(Guid id)
    {
        var member = await GetByIdAsync(id);
        if (member != null)
        {
            _context.GuildMembers.Remove(member);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}