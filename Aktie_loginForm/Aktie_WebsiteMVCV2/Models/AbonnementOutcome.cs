namespace Aktie_WebsiteMVCV2.Models
{
    public class AbonnementOutcome
    {
        public string Aktiepakke { get; set; }
        public int KategoriId { get; set; }
        public string Message { get; set; }

        public AbonnementOutcome(string aktiepakke, int kategoriId, string message)
        {
            Aktiepakke = aktiepakke;
            KategoriId = kategoriId;
            Message = message;
        }
    }
}