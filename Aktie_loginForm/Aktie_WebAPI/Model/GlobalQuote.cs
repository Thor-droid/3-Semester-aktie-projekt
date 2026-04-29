using System.Text.Json.Serialization;

public class GlobalQuote
{
    [JsonPropertyName("01. symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("05. price")]
    public string Price { get; set; }

    [JsonPropertyName("10. change percent")]
    public string ChangePercent { get; set; }
}