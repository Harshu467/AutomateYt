using Infrastructure;

namespace Application.Jobs;

public class VideoJob
{
    private readonly AppDbContext _db;
    private readonly VideoPipelineService _pipeline;

    public VideoJob(AppDbContext db, VideoPipelineService pipeline)
    {
        _db = db;
        _pipeline = pipeline;
    }

    public async Task Run(int taskId)
    {
        var task = await _db.VideoTasks.FindAsync(taskId);
        if (task == null) return;

        try
        {
            task.Status = "Processing";
            await _db.SaveChangesAsync();

            var url = await _pipeline.GenerateAsync(task.Topic);

            task.VideoUrl = url;
            task.Status = "Completed";
        }
        catch
        {
            task.Status = "Failed";
        }

        await _db.SaveChangesAsync();
    }
}