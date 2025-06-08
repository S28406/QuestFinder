using Mas_Project.Enums;
using Mas_Project.Models;

namespace Mas_Project.Models;

public class Quest
{
    public Guid QuestID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MinNumberOfParticipants { get; set; }
    public int MinRank { get; set; }
    public double DurationHours { get; set; }
    public DateTime EstimatedEndDate { get; set; }
    public string Reward { get; set; }
    public int Priority { get; set; }
    public QuestType Type { get; set; }
    public string Requirements { get; set; }
    public QuestStatus Status { get; set; }

    public Quest(Guid questId, string title, string description, int minParticipants, int minRank,
        double durationHours, string reward, int priority, QuestType type,
        string requirements, QuestStatus status)
    {
        QuestID = questId;
        Title = title;
        Description = description;
        MinNumberOfParticipants = minParticipants;
        MinRank = minRank;
        DurationHours = durationHours;
        EstimatedEndDate = DateTime.Now.AddHours(durationHours);
        Reward = reward;
        Priority = priority;
        Type = type;
        Requirements = requirements;
        Status = status;
    }

    public bool CheckPlayerReq(GuildMember player) =>
        player.Rank >= MinRank;

    public bool CheckTeamReq(List<GuildMember> team) =>
        team.Count >= MinNumberOfParticipants;

    public void UpdateStatus(QuestStatus newStatus) =>
        Status = newStatus;
}