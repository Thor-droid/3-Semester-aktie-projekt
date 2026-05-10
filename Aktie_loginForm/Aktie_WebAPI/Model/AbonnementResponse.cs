namespace Aktie_WebAPI.Model
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