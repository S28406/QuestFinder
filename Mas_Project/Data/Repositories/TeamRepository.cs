using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly DBContext _context;

    public TeamRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }

    public async Task UpdateAsync(Team team)
    {
        _context.Teams.Update(team);
    }

    public async Task DeleteAsync(Guid id)
    {
        var team = await GetByIdAsync(id);
        if (team != null)
        {
            _context.Teams.Remove(team);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}