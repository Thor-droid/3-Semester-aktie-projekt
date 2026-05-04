namespace Aktie_WebsiteMVCV2.Models.AktiePakkerModels
{
    public class Aktiepakke
    {
        public int KategoriId { get; }
        public int MaxUsers { get; }
        public List<string> Symbols { get; }

        public Aktiepakke(int kategoriId, int maxUsers, List<string> symbols)
        {
            KategoriId = kategoriId;
            MaxUsers = maxUsers;
            Symbols = symbols;
        }
    }
}