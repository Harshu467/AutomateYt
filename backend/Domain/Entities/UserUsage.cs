namespace Domain.Entities;

public class UserUsage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UsageType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    public string? ContextRef { get; set; }

    public User? User { get; set; }
}