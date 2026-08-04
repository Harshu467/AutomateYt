using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<VideoTask> VideoTasks => Set<VideoTask>();
    public DbSet<GeneratedAsset> GeneratedAssets => Set<GeneratedAsset>();
    public DbSet<UserUsage> UserUsages => Set<UserUsage>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VideoTask>(entity =>
        {
            entity.HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(t => t.Assets)
                .WithOne(a => a.Task)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<GeneratedAsset>(entity =>
        {
            entity.HasOne(a => a.Project)
                .WithMany(p => p.Assets)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserUsage>(entity =>
        {
            entity.HasOne(u => u.User)
                .WithMany(user => user.Usages)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}