using Mas_Project.Data.DTOs;
using Mas_Project.Data.Repositories;
using Mas_Project.Enums;
using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Services;

public class GuildMemberService
{
    private readonly IGuildMemberRepository _memberRepo;

    public GuildMemberService(IGuildMemberRepository memberRepo)
    {
        _memberRepo = memberRepo;
    }

    public async Task PromoteMemberAsync(Guid promoterId, Guid targetId)
    {
        var promoter = await _memberRepo.GetByIdAsync(promoterId);
        var target = await _memberRepo.GetByIdAsync(targetId);

        if (promoter == null || target == null)
            throw new ArgumentException("One or both guild members not found.");

        if (!promoter.MemberRoles.Contains(MemberRole.GuildMaster))
            throw new InvalidOperationException("Only a Guild Master can promote.");

        target.Promote();
        await _memberRepo.UpdateAsync(target);
        await _memberRepo.SaveChangesAsync();
    }
    
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
        int numberOfParticipants = quest.DateTakens
            .Count(dt => dt.Date.AddHours(quest.DurationHours) > DateTime.Now);
        if (quest.Status == QuestStatus.Created) quest.Status = QuestStatus.Taken;
        if(numberOfParticipants + 1 == quest.MaxNumberOfParticipants) quest.Status = QuestStatus.Full;
        await _memberRepo.SaveChangesAsync();
    }
    public async Task<GuildMember?> GetByIdAsync(Guid id)
    {
        return await (_memberRepo as GuildMemberRepository)?._context
            .GuildMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(gm => gm.UserID == id);
    }

    public Task<IEnumerable<GuildMemberDTO>> GetMemberDTOs()
    {
        return _memberRepo.GetAllIdsAsync();
    }
    public Guid GetTestUser()
    {
        var user = _memberRepo.GetAllIdsAsync().Result.OrderBy(gm => gm.Username).First().Id;
        return user;
    }
    
    
}