using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<VideoTask> VideoTasks => Set<VideoTask>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}