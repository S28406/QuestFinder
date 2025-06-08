using System.ComponentModel.DataAnnotations;

namespace Mas_Project.Models;


public class Customer : User
{
    [Required]
    public int ReputationScore { get; set; }
    
    [Required]
    public DateTime RegistrationDate { get; set; }
    
    public ICollection<Quest> Quests { get; set; } = new List<Quest>();

    public Customer(string username, Guid userId, string? email, int reputationScore, DateTime registrationDate)
        : base(username, userId, email)
    {
        ReputationScore = reputationScore;
        RegistrationDate = registrationDate;
    }
}