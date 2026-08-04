namespace Domain.Entities;

public class SubscriptionPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MaxProjects { get; set; }
    public int MaxVideoTasksPerMonth { get; set; }
    public string AllowedFeatures { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
