using Monarchs.Helpers;

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