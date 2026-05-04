namespace Aktie_WebsiteMVCV2.DTO.Stock
{
    public class AktiepakkeView
    {
        public int KategoriId { get; }
        public int CurrentUsers { get; }
        public int MaxUsers { get; }
        public int LedigePladser => MaxUsers - CurrentUsers;
        public List<GlobalQuoteDto> Stocks { get; }

        public AktiepakkeView(int kategoriId,int currentUsers,int maxUsers, List<GlobalQuoteDto> stocks)
        {
            KategoriId = kategoriId;
            CurrentUsers = currentUsers;
            MaxUsers = maxUsers;
            Stocks = stocks ?? new List<GlobalQuoteDto>();
        }
    }
}