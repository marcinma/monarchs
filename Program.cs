

using System.Text.Json;

var uri = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";
var httpClient = new HttpClient();
var kingsProvider = new KingsProvider(httpClient);
var kings = await kingsProvider.GetKingsAsync(uri);

Console.WriteLine($"There was {kings.Count} kings");
var longestRullingKing = KingsStatistics.GetLongestRullingKing(kings);
Console.WriteLine($"King {longestRullingKing.Name} was longest rulling king and rulled for {longestRullingKing.Years} years");
var longestRullingHouse = KingsStatistics.GetLongestRullingHouse(kings);
Console.WriteLine($"{longestRullingHouse.Name} was longest rulling house for {longestRullingHouse.Years} years");
var mostCommonName = KingsStatistics.GetMostCommonName(kings);
Console.WriteLine($"Most common name was {mostCommonName}");

public class KingsProvider
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

public class KingResponse
{
    public int id { get; set; }
    public string nm { get; set; } = string.Empty;
    public string cty { get; set; } = string.Empty;
    public string hse { get; set; } = string.Empty;
    public string yrs { get; set; } = string.Empty;

    public int FirstYearOfRulling => strYearsToInt(yrs.Split('-')[0]);
    public int LastYearOfRulling => yrs.Split('-').Length > 1 ? strYearsToInt(yrs.Split('-')[1], FirstYearOfRulling) : FirstYearOfRulling;
    public string FirstName => nm.Split(" ")[0];
    private int strYearsToInt(string? years, int defaultYears = 0) => string.IsNullOrEmpty(years) ? defaultYears : int.Parse(years);
}

public record KingsStatisticResult(string Name, int Years);
public record HouseStatisticResult(string Name, int Years);

public static class KingsStatistics
{
    public static string GetMostCommonName(List<KingResponse> kings)
    {
        var groupByFirstName = kings.GroupBy(k => k.FirstName);
        var mostNameOccurences = groupByFirstName.Max(g => g.Count());
        var kingsWithSameName = groupByFirstName.First(g => g.Count() == mostNameOccurences);
        return kingsWithSameName.Key;
    }

    public static KingsStatisticResult GetLongestRullingKing(List<KingResponse> kings)
    {
        var calculateYearsOfRulling = (KingResponse king) =>
        {
            return king.LastYearOfRulling - king.FirstYearOfRulling;
        };

        var longestRulling = kings.Max(calculateYearsOfRulling);
        var king = kings.First(k => calculateYearsOfRulling(k) == longestRulling);
        return new KingsStatisticResult(king.nm, longestRulling);
    }

    public static HouseStatisticResult GetLongestRullingHouse(List<KingResponse> kings)
    {
        var houseGroup = kings.GroupBy(k => k.hse, k => k, (house, kings) => new {
            Key = house,
            Kings = kings

        }); ;
        var houseToYears = new Dictionary<string, int>();
        foreach (var result in houseGroup)
        {
            var kingsRulling = result.Kings.OrderBy(k => k.yrs);
            var firstKing = kingsRulling.First();
            var lastKing = kingsRulling.Last();
            var totalYearsOfRulling = lastKing.LastYearOfRulling - firstKing.FirstYearOfRulling;
            houseToYears.Add(result.Key, totalYearsOfRulling);
        }

        var longestRulling = houseToYears.Max(h => h.Value);
        var house = houseToYears.First(h => h.Value == longestRulling);
        return new HouseStatisticResult(house.Key, longestRulling);
    }
}