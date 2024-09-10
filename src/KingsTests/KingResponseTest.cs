

using Models;

namespace KingsTests;
public class KingResponseTest
{
    [Fact]
    public void FirstYearOfRulling_Returns0ForEmptyData()
    {
        // Arrange
        var king = new KingResponse { yrs = "" };

        // Act & Assert
        Assert.True(king.FirstYearOfRulling == 0);
    }

    [Fact]
    public void LastYearOfRullingg_Returns0ForEmptyData()
    {
        // Arrange
        var king = new KingResponse { yrs = "" };

        // Act & Assert
        Assert.True(king.LastYearOfRulling == 0);
    }

    [Fact]
    public void LastYearOfRullingg_ReturnsFirstYearForMisingLast()
    {
        // Arrange
        var king = new KingResponse { yrs = "99-" };

        // Act & Assert
        Assert.True(king.LastYearOfRulling == 99);
    }

    [Fact]
    public void LastYearOfRullingg_ReturnsFirstYearForOnlyFirstYear()
    {
        // Arrange
        var king = new KingResponse { yrs = "99" };

        // Act & Assert
        Assert.True(king.LastYearOfRulling == 99);
    }
}