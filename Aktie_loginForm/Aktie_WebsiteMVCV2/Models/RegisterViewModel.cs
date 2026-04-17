using System.ComponentModel.DataAnnotations;

namespace Aktie_WebsiteMVCV2.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "KundeNavn er påkrævet")]
        public string KundeNavn { get; set; } = "";

        [Required(ErrorMessage = "Email er påkrævet")]
        [EmailAddress(ErrorMessage = "Ugyldig email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password er påkrævet")]
        public string Password { get; set; } = "";

        public string? ErrorMessage { get; set; }
    }
}