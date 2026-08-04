namespace Domain.Entities;

public class VideoTask
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Stage { get; set; } = "Queued";
    public string Status { get; set; } = "Pending";
    public int RetryCount { get; set; }
    public string? ErrorMessage { get; set; }
    public string? InputPayload { get; set; }
    public string? OutputPayload { get; set; }
    public string? VideoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public Project? Project { get; set; }
    public ICollection<GeneratedAsset> Assets { get; set; } = new List<GeneratedAsset>();
}