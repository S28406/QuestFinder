namespace Mas_Project.Models;

public class Achievement
{
    public Guid AchievementID { get; set; }
    public string Name { get; set; }
    public int ExperienceReward { get; set; }

    public Achievement(Guid achievementId, string name, int experienceReward)
    {
        AchievementID = achievementId;
        Name = name;
        ExperienceReward = experienceReward;
    }
}