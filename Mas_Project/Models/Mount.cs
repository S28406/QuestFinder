using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;


public class Mount
{
    [Key]
    [Required]
    public Guid MountID { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    [Required]
    [MinLength(3)]
    public string Type { get; set; }
    
    public Guid GuildMemberId { get; set; }
    public GuildMember GuildMember { get; set; }

    public Mount(Guid mountId, string name, string type)
    {
        MountID = mountId;
        Name = name;
        Type = type;
    }
}