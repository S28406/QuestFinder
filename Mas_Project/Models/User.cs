using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;

public abstract class User
{
    [Required]
    public string Username { get; set; }
    [Key]
    [Required]
    public Guid UserID { get; set; }
    public string? Email { get; set; }

    public User(string username, Guid userId, string? email = null)
    {
        Username = username;
        UserID = userId;
        Email = email;
    }
}