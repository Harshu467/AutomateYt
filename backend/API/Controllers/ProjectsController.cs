using Application.Services;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectsController(AppDbContext dbContext)
    {
        _projectService = new ProjectService(dbContext);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Create([FromBody] CreateProjectRequest request)
    {
        var project = await _projectService.CreateProjectAsync(1, request.Title, request.Brief, request.TargetAudience);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
        var project = await _projectService.GetProjectAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }
}

public class CreateProjectRequest
{
    public string Title { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string TargetAudience { get; set; } = string.Empty;
}
