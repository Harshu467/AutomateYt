namespace Application.Interfaces;

public interface IOpenAIService
{
    Task<string> GenerateScriptAsync(string topic);
}