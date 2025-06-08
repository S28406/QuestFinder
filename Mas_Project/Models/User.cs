namespace Mas_Project.Models;

public abstract class User
{
    public string Username { get; set; }
    public Guid UserID { get; set; }
    public string? Email { get; set; }

    public User(string username, Guid userId, string? email = null)
    {
        Username = username;
        UserID = userId;
        Email = email;
    }
}