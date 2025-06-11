namespace Mas_Project.Data.DTOs;

public class QuestDTO
{
    public Guid QuestID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Reward { get; set; }
    public int MinRank { get; set; }
    public double DurationHours { get; set; }
    public string Requirements { get; set; }
    public List<ParticipantDTO> Participants { get; set; } = new();
}

public class ParticipantDTO
{
    public Guid UserID { get; set; }
    public string Username { get; set; }
}