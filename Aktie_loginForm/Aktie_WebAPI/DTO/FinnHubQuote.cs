using System.Text.Json.Serialization;

namespace Aktie_WebAPI.DTO
{
    
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