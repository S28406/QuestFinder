using Mas_Project.Data.Repositories;
using Mas_Project.Enums;
using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Services;

public class GuildMemberService
{
    private readonly IGuildMemberRepository _memberRepo;

    public GuildMemberService(IGuildMemberRepository memberRepo, ITeamRepository teamRepo)
    {
        _memberRepo = memberRepo;
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
    //
    // public async Task<Team> CreateTeamAsync(Guid managerId, int rank)
    // {
    //     var manager = await _memberRepo.GetByIdAsync(managerId);
    //     if (manager == null)
    //         throw new ArgumentException("Guild manager not found.");
    //
    //     if (manager.MemberRole != MemberRole.GuildManager)
    //         throw new InvalidOperationException("Only a Guild Manager can create teams.");
    //
    //     var team = new Team(Guid.NewGuid(), rank);
    //     team.AddMember(manager);
    //
    //     await _teamRepo.AddAsync(team);
    //     await _teamRepo.SaveChangesAsync();
    //
    //     return team;
    // }
    //
    // public async Task AddMemberToTeamAsync(Guid managerId, Guid memberId, Guid teamId)
    // {
    //     var manager = await _memberRepo.GetByIdAsync(managerId);
    //     var member = await _memberRepo.GetByIdAsync(memberId);
    //     var team = await _teamRepo.GetByIdAsync(teamId);
    //
    //     if (manager == null || member == null || team == null)
    //         throw new ArgumentException("Missing guild member or team.");
    //
    //     if (manager.MemberRole != MemberRole.GuildManager)
    //         throw new InvalidOperationException("Only a Guild Manager can add members to a team.");
    //
    //     team.AddMember(member);
    //
    //     member.TeamGuid = team.TeamID;
    //     await _memberRepo.UpdateAsync(member);
    //     await _teamRepo.UpdateAsync(team);
    //
    //     await _teamRepo.SaveChangesAsync();
    //     await _memberRepo.SaveChangesAsync();
    // }
    
    public async Task AssignQuestToMemberAsync(Guid memberId, Quest quest)
    {
        var member = await _memberRepo.GetByIdAsync(memberId);
        if (member == null) throw new ArgumentException("Member not found.");
        if (quest.Status == QuestStatus.Full) throw new ArgumentException("The quest is full");
        

        bool alreadyTaken = quest.DateTakens.Any(dt => dt.GuildMemberId == memberId);
        if (member.Rank < quest.MinRank)
        {
            throw new ArgumentException("Guild Member must be at least rank " + quest.MinRank + " to take the " +
                                        quest.Title +"quest.");
        }

        if (!alreadyTaken)
        {
            var dateTaken = new DateTaken(DateTime.UtcNow)
            {
                GuildMemberId = memberId,
                QuestId = quest.QuestID,
                GuildMember = member,
                Quest = quest
            };

            quest.DateTakens.Add(dateTaken);

        }
        else
        {
            throw new InvalidOperationException("This member has already taken the quest.");
        }
        int numberOfParticipants = quest.DateTakens.Count;
        if (quest.Status == QuestStatus.Created) quest.Status = QuestStatus.Taken;
        if(numberOfParticipants + 1 == quest.MaxNumberOfParticipants) quest.Status = QuestStatus.Completed;
        await _memberRepo.SaveChangesAsync();
    }
    public async Task<GuildMember?> GetByIdAsync(Guid id)
    {
        return await (_memberRepo as GuildMemberRepository)?._context
            .GuildMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(gm => gm.UserID == id);
    }
    public Guid GetTestUser()
    {
        var users = _memberRepo.GetAllAsync().Result.OrderBy(gm => gm.Username);
        // foreach(var u in users)
        // {
        //     Console.WriteLine(u.Username);
        // }
        var user = _memberRepo.GetAllAsync().Result.OrderBy(gm => gm.Username).First().UserID;
        Console.WriteLine(_memberRepo.GetAllAsync().Result.OrderBy(gm => gm.Username).First().Username);
        return user;
    }
}