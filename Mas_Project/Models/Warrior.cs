using System.ComponentModel.DataAnnotations;
using Mas_Project.Enums;

namespace Mas_Project.Models;

public class Warrior : GuildMember
{
    [Required]
    public int ArmorClass { get; set; }
    public Warrior(){}
    public Warrior(string username, string? email, int rank, int experiencePoints, MemberRole memberRole,
        int armorClass, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, email, rank, experiencePoints, memberRole, relic, leadershipStyle, assignedRoom)
    {
        ArmorClass = armorClass;
    }
}