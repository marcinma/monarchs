using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

public record KingsStatisticResult(string Name, int Years);
public record HouseStatisticResult(string Name, int Years);

namespace Monarchs.Helpers
{
    public static class KingsStatistics
    {
        public static string GetMostCommonName(List<KingResponse> kings)
        {
            var group = kings.GroupBy(k=>k.nm.Split(" ")[0]);
            var mostNameOccurences = group.Max(g=>g.Count());
            var king = group.First(g => g.Count() == mostNameOccurences);
            return king.Key;
        }

        public static KingsStatisticResult GetLongestRullingKing(List<KingResponse> kings)
        {
            var calculateYearsOfRulling = (KingResponse king) =>
            {
                return king.LastYearOfRulling - king.FirstYearOfRulling;
            };

            var longestRulling = kings.Max(calculateYearsOfRulling);
            var king = kings.First(k=>calculateYearsOfRulling(k)==longestRulling);
            return new KingsStatisticResult(king.nm, longestRulling);
        }

        public static HouseStatisticResult GetLongestRullingHouse(List<KingResponse> kings)
        {
            var houseGroup = kings.GroupBy(k => k.hse, k => k, (house, kings) => new { 
                Key = house,
                Kings = kings

            }); ;
            var houseToYears = new Dictionary<string, int>();
            foreach(var result in houseGroup)
            {
                var kingsRulling = result.Kings.OrderBy(k => k.yrs);
                var firstKing = kingsRulling.First();
                var lastKing = kingsRulling.Last();
                var totalYearsOfRulling = lastKing.LastYearOfRulling - firstKing.FirstYearOfRulling;
                houseToYears.Add(result.Key, totalYearsOfRulling);
            }

            var longestRulling = houseToYears.Max(h=>h.Value);
            var house = houseToYears.First(h => h.Value == longestRulling);
            return new HouseStatisticResult(house.Key, longestRulling);
        }
    }
}
