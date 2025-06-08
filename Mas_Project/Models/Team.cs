using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;

public class Team
{
    [Key]
    [Required]
    public Guid TeamID { get; set; }
    public int MaxNumberOfPlayers { get; set; } = 5;
    [Required]
    [Range(1, 5)]
    public int Rank { get; set; }

    public ICollection<GuildMember> Members { get; set; } = new List<GuildMember>();

    public Team(Guid teamId, int rank)
    {
        TeamID = teamId;
        Rank = rank;
    }

    public void AddMember(GuildMember member)
    {
        if (Members.Count < MaxNumberOfPlayers)
            Members.Add(member);
    }
}