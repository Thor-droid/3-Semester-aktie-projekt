using System.Text.Json.Serialization;

namespace Aktie_WebsiteMVCV2.DTO.Stock
{
    public class GlobalQuoteDto
    {
        [JsonPropertyName("01. symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("05. price")]
        public string Price { get; set; }

        [JsonPropertyName("10. change percent")]
        public string ChangePercent { get; set; }
    }
}