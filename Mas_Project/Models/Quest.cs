using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mas_Project.Enums;
using Mas_Project.Models;

namespace Mas_Project.Models;

public class Quest
{
    [Key]
    [Required]
    public Guid QuestID { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    [Required]
    [MinLength(3)]
    public string Description { get; set; }
    [Required]
    public int MaxNumberOfParticipants { get; set; }
    public int MinRank { get; set; }
    private double _DurationHours;
    [Required]
    public double DurationHours
    {
        get => _DurationHours;
        set
        {
            if (value < 1 || value > 240)
                throw new ArgumentException("Duration vannot be more than 240 hours");
            _DurationHours = value;
        }
    }
    public DateTime EstimatedEndDate => DateTime.Now.AddHours(DurationHours);
    [Required]
    [MinLength(3)]
    public string Reward { get; set; }
    [Required]
    public int Priority { get; set; }
    [Required]
    public QuestType Type { get; set; }
    [Required]
    [MinLength(3)]
    public string Requirements { get; set; }
    [Required]
    public QuestStatus Status { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid QuestBoardId { get; set; }
    [ForeignKey("QuestBoardId")]
    public QuestBoard QuestBoard { get; set; }
    public ICollection<DateTaken> DateTakens { get; set; } = new List<DateTaken>();
    
    
    public Quest(){}
    public Quest(Guid questId, string title, string description, int maxParticipants, int minRank,
        double durationHours, string reward, int priority, QuestType type,
        string requirements, QuestStatus status)
    {
        QuestID = questId;
        Title = title;
        Description = description;
        MaxNumberOfParticipants = maxParticipants;
        MinRank = minRank;
        DurationHours = durationHours;
        Reward = reward;
        Priority = priority;
        Type = type;
        Requirements = requirements;
        Status = status;
    }

    public void UpdateStatus(QuestStatus newStatus) =>
        Status = newStatus;
}