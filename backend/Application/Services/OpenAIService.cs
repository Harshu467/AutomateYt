using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Interfaces;

namespace Application.Services;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public OpenAIService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<string> GenerateScriptAsync(string topic)
    {
        var apiKey = _config["OpenAIKey"];

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var request = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "user", content = $"Create a viral YouTube script about {topic}" }
            }
        };

        var res = await _http.PostAsync("https://api.openai.com/v1/chat/completions",
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        var json = await res.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "";
    }
}