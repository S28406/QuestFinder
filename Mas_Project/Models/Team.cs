using System;
using System.Collections.Generic;

namespace Mas_Project.Models;

public class Team
{
    public Guid TeamID { get; set; }
    public int MaxNumberOfPlayers { get; set; } = 5;
    public int Rank { get; set; }

    public List<GuildMember> Members { get; set; } = new();

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