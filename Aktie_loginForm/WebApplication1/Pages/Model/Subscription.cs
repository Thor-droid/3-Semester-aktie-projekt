namespace Aktie_Website.Pages.Model
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        public int CustomerId { get; set; }

        public int CategoryId { get; set; } // 🔗 hvilken plan de har

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
