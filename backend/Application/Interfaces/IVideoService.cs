namespace Application.Interfaces;

public interface IVideoService
{
    Task<string> CreateVideoAsync(string audioFilePath);
}
