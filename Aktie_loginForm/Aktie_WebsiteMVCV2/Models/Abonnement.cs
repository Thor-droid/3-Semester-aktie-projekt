namespace Aktie_WebsiteMVCV2.Models
{
    public class Abonnement
    {
        public int Id { get; set; }
        public int KundeId { get; set; }
        public int AktiepakkeId { get; set; }
        public DateTime StartDato { get; set; }
    }
}
