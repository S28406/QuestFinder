using Mas_Project.Data.Repositories;
using Mas_Project.Models;
using Mas_Project.Data.Repositories.Interfaces;
using Mas_Project.Enums;

namespace Mas_Project.Services;

public class TeamService
{
    private readonly ITeamRepository _teamRepo;
    private readonly IGuildMemberRepository _memberRepo;

    public TeamService(ITeamRepository teamRepo, IGuildMemberRepository memberRepo)
    {
        _teamRepo = teamRepo;
        _memberRepo = memberRepo;
    }

    public async Task<Team?> GetTeamAsync(Guid teamId)
    {
        return await _teamRepo.GetByIdAsync(teamId);
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await _teamRepo.GetAllAsync();
    }

    public async Task<Team> CreateTeamAsync(int rank, Guid memberId)
    {
        var manager = await _memberRepo.GetByIdAsync(memberId);

        if (manager == null)
            throw new ArgumentException("Member not found.");

        if (manager.MemberRole != MemberRole.GuildManager)
            throw new InvalidOperationException("Only a Guild Manager can create teams.");
        
        var team = new Team(Guid.NewGuid(), rank);
        await _teamRepo.AddAsync(team);
        await _teamRepo.SaveChangesAsync();
        return team;
    }

    public async Task AddMemberToTeamAsync(Guid teamId, Guid memberId)
    {
        var manager = await _memberRepo.GetByIdAsync(memberId);

        if (manager == null)
            throw new ArgumentException("Member not found.");

        if (manager.MemberRole != MemberRole.GuildManager)
            throw new InvalidOperationException("Only a Guild Manager can create teams.");
        
        var team = await _teamRepo.GetByIdAsync(teamId);
        var member = await _memberRepo.GetByIdAsync(memberId);

        if (team == null || member == null)
            throw new ArgumentException("Team or member not found.");

        team.AddMember(member);
        await _teamRepo.UpdateAsync(team);
        await _teamRepo.SaveChangesAsync();
    }
}