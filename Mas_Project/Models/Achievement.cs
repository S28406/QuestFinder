using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;

public class Achievement
{
    [Key]
    [Required]
    public Guid AchievementID { get; set; }
    
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
    
    [Required]
    public int ExperienceReward { get; set; }

    public ICollection<DateUnlocked> DatesUnlocked { get; set; } = new List<DateUnlocked>();
    public Achievement() { }
    public Achievement(Guid achievementId, string name, int experienceReward)
    {
        AchievementID = achievementId;
        Name = name;
        ExperienceReward = experienceReward;
    }
}