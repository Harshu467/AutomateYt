namespace Domain.Entities;

public class GeneratedAsset
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int? TaskId { get; set; }
    public string AssetType { get; set; } = string.Empty;
    public string? StorageUrl { get; set; }
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project? Project { get; set; }
    public VideoTask? Task { get; set; }
}
