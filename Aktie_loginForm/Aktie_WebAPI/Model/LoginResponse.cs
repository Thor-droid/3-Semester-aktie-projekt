namespace Aktie_WebAPI.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public int KundeId { get; set; }
        public string Navn { get; set; }
        public int? AbonnementId { get; set; }

        public LoginResponse() { }

        public LoginResponse(bool success, int kundeId, string navn, int? abonnementId)
        {
            Success = success;
            KundeId = kundeId;
            Navn = navn;
            AbonnementId = abonnementId;
        }
    }
}