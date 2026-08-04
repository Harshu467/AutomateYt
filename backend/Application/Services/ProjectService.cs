using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProjectService
{
    private readonly AppDbContext _dbContext;

    public ProjectService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Project> CreateProjectAsync(int userId, string title, string brief, string targetAudience)
    {
        var project = new Project
        {
            UserId = userId,
            Title = title,
            Brief = brief,
            TargetAudience = targetAudience,
            Status = "Queued",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> GetProjectAsync(int id)
    {
        return await _dbContext.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Assets)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
