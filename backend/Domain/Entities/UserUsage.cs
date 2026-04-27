namespace Domain.Entities;

public class UserUsage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VideosGenerated { get; set; }
    public DateTime ResetDate { get; set; } = DateTime.UtcNow.Date;
}