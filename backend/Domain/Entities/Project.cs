namespace Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string TargetAudience { get; set; } = string.Empty;
    public string Status { get; set; } = "Queued";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public ICollection<VideoTask> Tasks { get; set; } = new List<VideoTask>();
    public ICollection<GeneratedAsset> Assets { get; set; } = new List<GeneratedAsset>();
}
