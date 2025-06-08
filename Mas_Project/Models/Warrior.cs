using Mas_Project.Enums;

namespace Mas_Project.Models;

public class Warrior : GuildMember
{
    public int ArmorClass { get; set; }

    public Warrior(string username, Guid userId, string? email, int rank, int experiencePoints, MemberRole memberRole,
        int armorClass, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, userId, email, rank, experiencePoints, memberRole, relic, leadershipStyle, assignedRoom)
    {
        ArmorClass = armorClass;
    }
}