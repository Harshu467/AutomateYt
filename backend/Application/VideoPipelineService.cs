using Application.Interfaces;

namespace Application;

public class VideoPipelineService
{
    private readonly IOpenAIService _ai;
    private readonly IVoiceService _voice;
    private readonly IVideoService _video;
    private readonly IStorageService _storage;

    public VideoPipelineService(
        IOpenAIService ai,
        IVoiceService voice,
        IVideoService video,
        IStorageService storage)
    {
        _ai = ai;
        _voice = voice;
        _video = video;
        _storage = storage;
    }

    public async Task<string> GenerateAsync(string topic)
    {
        var script = await _ai.GenerateScriptAsync(topic);
        var audio = await _voice.GenerateVoiceAsync(script);
        var video = await _video.CreateVideoAsync(audio);
        var url = await _storage.UploadAsync(video);

        return url;
    }
}