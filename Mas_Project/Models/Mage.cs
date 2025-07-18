﻿using System.ComponentModel.DataAnnotations;
using Mas_Project.Enums;

namespace Mas_Project.Models;

public class Mage : GuildMember
{
    [Required]
    public int MP { get; set; }
    public Mage(){}
    public Mage(string username, string? email, int rank, int experiencePoints, HashSet<MemberRole> memberRole,
        int mp, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, email, rank, experiencePoints, memberRole, relic, leadershipStyle, assignedRoom)
    {
        MP = mp;
    }
}