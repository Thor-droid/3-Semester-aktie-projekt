namespace Aktie_WebsiteMVCV2.Models
{
    public class RegisterViewModel
    {
        public string KundeNavn { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}