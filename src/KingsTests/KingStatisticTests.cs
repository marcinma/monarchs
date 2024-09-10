using Models;
using Monarchs.Helpers;
namespace KingsTests
{
    public class KingStatisticTests
    {
        readonly List<KingResponse> baseTestData = new List<KingResponse>
            {
                new KingResponse
                {
                    nm="A",
                    hse="House-A",
                    yrs="0-2"
                },
                new KingResponse
                {
                    nm="B I",
                    hse="House-A",
                    yrs="2-4"
                },
                new KingResponse
                {
                    nm="B II",
                    hse="House-A",
                    yrs="4-10"
                },
                new KingResponse
                {
                    nm="D",
                    hse="House-B",
                    yrs="0-7"
                },
                new KingResponse
                {
                    nm="E",
                    hse="House-B",
                    yrs="7-9"
                }
            };

        [Fact]
        public void GetLongestRullingKing_ReturnsLongestRuller()
        {
            // Arrange
            var kings = baseTestData;

            // Act
            var result = KingsStatistics.GetLongestRullingKing(kings);


            // Assert
            Assert.True(result.Years == 7);
            Assert.True(result.Name == "D");
        }

        [Fact]
        public void GetLongestRullingHouse_ReturnsLongestRullingHouse()
        {
            // Arrange
            var kings = baseTestData;

            // Act
            var result = KingsStatistics.GetLongestRullingHouse(kings);


            // Assert
            Assert.True(result.Years == 10);
            Assert.True(result.Name == "House-A");
        }

        [Fact]
        public void GetMostCommonName_ReturnsNameWithMostOccurences()
        {
            // Arrange
            var kings = baseTestData;

            // Act
            var result = KingsStatistics.GetMostCommonName(kings);


            // Assert
            Assert.True(result == "B");
        }
    }
}