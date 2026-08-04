namespace Application.Interfaces;

public interface IStorageService
{
    Task<string> UploadAsync(string videoPath);
}
