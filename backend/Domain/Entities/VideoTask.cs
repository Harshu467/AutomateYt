namespace Domain.Entities;

public class VideoTask
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Topic { get; set; } = "";
    public string Status { get; set; } = "Pending";
    public string? VideoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}