namespace Application.Interfaces;

public interface IVoiceService
{
    Task<string> GenerateVoiceAsync(string text);
}