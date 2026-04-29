namespace Aktie_WebAPI.Models
{
    public class AbonnementResponse
    {
        public AbonnementResponse(int kategoriId)
        {
            KategoriId = kategoriId;
        }
        public int KategoriId { get; set; }
    }
}