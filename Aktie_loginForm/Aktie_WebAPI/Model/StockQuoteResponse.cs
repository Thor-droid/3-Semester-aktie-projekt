using System.Text.Json.Serialization;

namespace Aktie_WebsiteMVCV2.Models
{
    public class StockQuoteResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}