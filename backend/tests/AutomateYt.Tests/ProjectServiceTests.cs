using Application.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AutomateYt.Tests;

public class ProjectServiceTests
{
    [Fact]
    public async Task CreateProjectAsync_ShouldPersistProjectAndDefaultStatus()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var dbContext = new AppDbContext(options);
        var service = new ProjectService(dbContext);

        var result = await service.CreateProjectAsync(1, "AI Fitness Shorts", "Create a short about beginner workouts", "beginners");

        Assert.NotNull(result);
        Assert.Equal("Queued", result.Status);
        Assert.Equal(1, await dbContext.Projects.CountAsync());
    }
}
