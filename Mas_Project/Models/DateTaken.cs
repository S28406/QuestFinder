using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;

public class DateTaken
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required]
    public Guid GuildMemberId { get; set; }
    public GuildMember GuildMember { get; set; }
    
    [Required]
    public Guid QuestId { get; set; }
    public Quest Quest { get; set; }

    public DateTaken(DateTime date)
    {
        Date = date;
    }
}