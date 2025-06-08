using System.ComponentModel.DataAnnotations;
using Mas_Project.Enums;

namespace Mas_Project.Models;

public class Priest : GuildMember
{
    [Required]
    public int DivinePower { get; set; }
    public Priest(){}
    public Priest(string username, Guid userId, string? email, int rank, int experiencePoints, MemberRole memberRole,
        int divinePower, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, userId, email, rank, experiencePoints, memberRole, relic, leadershipStyle, assignedRoom)
    {
        DivinePower = divinePower;
    }
}