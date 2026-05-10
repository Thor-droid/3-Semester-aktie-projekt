namespace Aktie_WebAPI.Model
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public int KundeId { get; set; }
        public string Navn { get; set; }
        public int? AbonnementId { get; set; }

        public bool IsAdmin { get; set; } 

        public LoginResponse() { }

        public LoginResponse(bool success, int kundeId, string navn, int? abonnementId, bool isAdmin)
        {
            Success = success;
            KundeId = kundeId;
            Navn = navn;
            AbonnementId = abonnementId;
            IsAdmin = isAdmin;
        }
    }
}