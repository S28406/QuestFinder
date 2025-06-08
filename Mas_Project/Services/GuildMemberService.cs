using Mas_Project.Data.Repositories;
using Mas_Project.Enums;
using Mas_Project.Models;

namespace Mas_Project.Services;

public class GuildMemberService
{
    private readonly IGuildMemberRepository _memberRepo;
    private readonly ITeamRepository _teamRepo;

    public GuildMemberService(IGuildMemberRepository memberRepo, ITeamRepository teamRepo)
    {
        _memberRepo = memberRepo;
        _teamRepo = teamRepo;
    }

    public async Task PromoteMemberAsync(Guid promoterId, Guid targetId)
    {
        var promoter = await _memberRepo.GetByIdAsync(promoterId);
        var target = await _memberRepo.GetByIdAsync(targetId);

        if (promoter == null || target == null)
            throw new ArgumentException("One or both guild members not found.");

        if (promoter.MemberRole != MemberRole.GuildMaster)
            throw new InvalidOperationException("Only a Guild Master can promote.");

        target.Promote(); // calls model method
        await _memberRepo.UpdateAsync(target);
        await _memberRepo.SaveChangesAsync();
    }

    public async Task<Team> CreateTeamAsync(Guid managerId, int rank)
    {
        var manager = await _memberRepo.GetByIdAsync(managerId);
        if (manager == null)
            throw new ArgumentException("Guild manager not found.");

        if (manager.MemberRole != MemberRole.GuildManager)
            throw new InvalidOperationException("Only a Guild Manager can create teams.");

        var team = new Team(Guid.NewGuid(), rank);
        team.AddMember(manager);

        await _teamRepo.AddAsync(team);
        await _teamRepo.SaveChangesAsync();

        return team;
    }

    public async Task AddMemberToTeamAsync(Guid managerId, Guid memberId, Guid teamId)
    {
        var manager = await _memberRepo.GetByIdAsync(managerId);
        var member = await _memberRepo.GetByIdAsync(memberId);
        var team = await _teamRepo.GetByIdAsync(teamId);

        if (manager == null || member == null || team == null)
            throw new ArgumentException("Missing guild member or team.");

        if (manager.MemberRole != MemberRole.GuildManager)
            throw new InvalidOperationException("Only a Guild Manager can add members to a team.");

        team.AddMember(member);

        await _teamRepo.UpdateAsync(team);
        await _teamRepo.SaveChangesAsync();
    }
}