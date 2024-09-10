namespace Models;

public class KingResponse
{
    public int id { get; set; }
    public string nm { get; set; } = string.Empty;
    public string cty { get; set; } = string.Empty;
    public string hse { get; set; } = string.Empty;
    public string yrs { get; set; } = string.Empty;

    public int FirstYearOfRulling => strYearsToInt(yrs.Split('-')[0]);
    public int LastYearOfRulling => yrs.Split('-').Length > 1 ? strYearsToInt(yrs.Split('-')[1], FirstYearOfRulling) : FirstYearOfRulling;

    private int strYearsToInt(string? years, int defaultYears=0) => string.IsNullOrEmpty(years) ? defaultYears : int.Parse(years);
}
