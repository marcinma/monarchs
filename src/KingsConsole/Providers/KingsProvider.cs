using Models;
using System.Text.Json;
public class KingsProvider : IKingsProvider
{
    private readonly HttpClient _httpClient;

    public KingsProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<KingResponse>> GetKingsAsync(string uri)
    {

        var responseString = await _httpClient.GetStringAsync(uri);

        var kings = JsonSerializer.Deserialize<IEnumerable<KingResponse>>(responseString);
        return kings is null ? new List<KingResponse>() : kings.ToList();
    }
}