using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;

public class DateUnlocked
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required]
    public Guid GuildMemberId { get; set; }
    public GuildMember GuildMember { get; set; }
    
    [Required]
    public Guid AchievementId { get; set; }
    public Achievement Achievement { get; set; }
    public DateUnlocked(DateTime date)
    {
        Date = date;
    }
}