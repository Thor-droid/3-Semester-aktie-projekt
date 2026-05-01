using System.Text.Json.Serialization;

public class GlobalQuote
{
    public string Symbol { get; set; }
    public string Price { get; set; }
    public string ChangePercent { get; set; }
}