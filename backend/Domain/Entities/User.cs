namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Plan { get; set; } = "Free";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<UserUsage> Usages { get; set; } = new List<UserUsage>();
}