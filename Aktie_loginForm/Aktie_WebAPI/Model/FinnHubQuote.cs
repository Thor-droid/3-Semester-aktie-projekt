using System.Text.Json.Serialization;

namespace Aktie_WebsiteMVCV2.Models
{
    // 🔥 New Finnhub response model
    public class FinnhubQuote
    {
        [JsonPropertyName("c")]
        public double CurrentPrice { get; set; }

        [JsonPropertyName("d")]
        public double Change { get; set; }

        [JsonPropertyName("dp")]
        public double ChangePercent { get; set; }
    }
}