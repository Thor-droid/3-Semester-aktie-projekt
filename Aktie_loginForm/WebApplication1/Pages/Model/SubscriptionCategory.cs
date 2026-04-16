namespace Aktie_Website.Pages.Model
{
    public class SubscriptionCategory
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }      // fx Basic, Pro, Premium
        public decimal PricePerMonth { get; set; }

        public int MaxUsers { get; set; }
    }
}
