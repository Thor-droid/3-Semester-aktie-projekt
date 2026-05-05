namespace Aktie_WebAPI.Model
{
    public class PriceAlert
    {
        public int Id { get; set; }

        // Which user set the alert (fx "user123")
        public string UserId { get; set; }

        // Stock (fx "AAPL", "TSLA")
        public string StockSymbol { get; set; }

        // Price (when it is reached or goes below)
        public decimal TargetPrice { get; set; }

        // Whether the alert has already been triggered
        public bool IsTriggered { get; set; } = false;

        // When the alert was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // When it was triggered
        public DateTime? TriggeredAt { get; set; }
    }
}
