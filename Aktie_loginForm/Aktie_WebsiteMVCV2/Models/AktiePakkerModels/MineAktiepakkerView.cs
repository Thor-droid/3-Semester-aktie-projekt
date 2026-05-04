using Aktie_WebsiteMVCV2.DTO.Abonnement;
using Aktie_WebsiteMVCV2.DTO.Stock;

namespace Aktie_WebsiteMVCV2.Models.AktiePakkerModels
{
    public class MineAktiepakkerView
    {
        public AbonnementResponse Abonnement { get; }
        public List<GlobalQuoteDto> Stocks { get; }

        public MineAktiepakkerView(
            AbonnementResponse abonnement,
            List<GlobalQuoteDto> stocks)
        {
            Abonnement = abonnement;
            Stocks = stocks ?? new List<GlobalQuoteDto>();
        }
    }
}