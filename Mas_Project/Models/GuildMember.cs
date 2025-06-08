using Mas_Project.Enums;
using Mas_Project.Interfaces;

namespace Mas_Project.Models;

public class GuildMember : User, IGuildManager, IGuildMaster
{
    public int Rank { get; set; }
    private int _ExperiencePoints;

    public int ExperiencePoints
    {
        get => _ExperiencePoints;
        set
        {
            if (value <= _ExperiencePoints)
                throw new ArgumentException("Experience points cannot be decreased.");
            _ExperiencePoints = value;
        }
    }

    public MemberRole MemberRole { get; set; }
    public string? Relic { get; set; }
    public string? LeadershipStyle { get; set; }
    public int? AssignedRoom { get; set; }

    public GuildMember(string username, Guid userId, string? email, int rank, int experiencePoints,
        MemberRole memberRole, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, userId, email)
    {
        Rank = rank;
        ExperiencePoints = experiencePoints;
        MemberRole = memberRole;
        Relic = relic;
        LeadershipStyle = leadershipStyle;
        AssignedRoom = assignedRoom;
    }

    public void Promote()
    {
        Rank += 1;
    }

    public void Attack() { /* implement local logic */ }
    public void ViewQuests() { /* implement local logic */ }
    public void TakeQuest() { /* implement local logic */ }
}