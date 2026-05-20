using System.Text.Json.Serialization;

namespace Aktie_WebAPI.Model
{
    public class StockQuoteResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}