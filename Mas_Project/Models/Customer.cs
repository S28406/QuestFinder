namespace Mas_Project.Models;


public class Customer : User
{
    public int ReputationScore { get; set; }
    public DateTime RegistrationDate { get; set; }

    public Customer(string username, Guid userId, string? email, int reputationScore, DateTime registrationDate)
        : base(username, userId, email)
    {
        ReputationScore = reputationScore;
        RegistrationDate = registrationDate;
    }
}