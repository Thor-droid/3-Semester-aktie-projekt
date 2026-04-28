using Aktie_WebsiteMVCV2.DTO.Stock;
using System.Text.Json.Serialization;

public class StockQuoteResponse
{
    [JsonPropertyName("Global Quote")]
    public GlobalQuoteDto GlobalQuote { get; set; }
}