using Mas_Project.Enums;

namespace Mas_Project.Models;

public class Mage : GuildMember
{
    public int MP { get; set; }

    public Mage(string username, Guid userId, string? email, int rank, int experiencePoints, MemberRole memberRole,
        int mp, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, userId, email, rank, experiencePoints, memberRole, relic, leadershipStyle, assignedRoom)
    {
        MP = mp;
    }
}