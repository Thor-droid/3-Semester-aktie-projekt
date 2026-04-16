namespace Aktie_Website.Pages.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }

        // OBS: i rigtig system skal dette være hashed password
        public string Password { get; set; }
    }
}
