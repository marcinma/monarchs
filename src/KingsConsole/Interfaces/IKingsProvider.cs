using Models;

interface IKingsProvider
{
    Task<List<KingResponse>> GetKingsAsync(string uri);
}
