namespace Aktie_WebsiteMVCV2.Models.AccountModels
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? AbonnomentID { get; set; }
    }
}