﻿using System.ComponentModel.DataAnnotations;
using Mas_Project.Enums;
using Mas_Project.Interfaces;

namespace Mas_Project.Models;

public class GuildMember : User, IGuildManager, IGuildMaster
{
    [Required]
    [Range(1, 5)]
    public int Rank { get; set; }
    private int _ExperiencePoints;
    [Required]
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

    [Required] public ICollection<MemberRole> MemberRoles { get; set; } = new HashSet<MemberRole>();
    public string? Relic { get; set; }
    public string? LeadershipStyle { get; set; }
    public int? AssignedRoom { get; set; }
    
    public ICollection<Mount> Mounts { get; set; } = new List<Mount>();
    public Guid TeamGuid { get; set; }
    public Team Team { get; set; }
    public ICollection<DateTaken> DatesTaken { get; set; } = new List<DateTaken>();
    public ICollection<DateUnlocked> DatesUnlocked { get; set; } = new List<DateUnlocked>(); 
    public GuildMember(){}

    public GuildMember(string username, string? email, int rank, int experiencePoints,
        ICollection<MemberRole> memberRole, string? relic = null, string? leadershipStyle = null, int? assignedRoom = null)
        : base(username, email)
    {
        Rank = rank;
        ExperiencePoints = experiencePoints;
        MemberRoles = memberRole;
        Relic = relic;
        LeadershipStyle = leadershipStyle;
        AssignedRoom = assignedRoom;
    }

    public void Promote()
    {
        Rank += 1;
    }

    public void Attack() { Console.WriteLine("The member attacked"); }
}