namespace Aktie_WebsiteMVCV2.Models
{
    public class AktiePakke
    {
        public int Id { get; set; }
        public string PakkeNavn { get; set; }

        public int MaxUsers { get; set; }
        public int CurrentUsers { get; set; }

        public int KategoriId { get; set; }
    }
}
