using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Models.AktiePakkerModels;
using Aktie_WebsiteMVCV2.Services;

namespace Aktie_WebsiteMVCV2.BusinessLogicLayer
{
    public class AktiepakkeLogic
    {
        private readonly AbonnementApiService _abonnementService;
        private readonly StockApiService _stockService;

        public AktiepakkeLogic(
            AbonnementApiService abonnementService,
            StockApiService stockService)
        {
            _abonnementService = abonnementService;
            _stockService = stockService;
        }

        public async Task<AktiepakkeView?> GetAktiepakke(int kategoriId)
        {
            var pakke = GetPakke(kategoriId);

            if (pakke == null)
                return null;

            int currentUsers = await _abonnementService.GetCurrentUsersByKategoriId(kategoriId);

            var stocks = new List<GlobalQuoteDto>();

            foreach (var symbol in pakke.Symbols)
            {
                var stock = await _stockService.GetStock(symbol);

                if (stock != null)
                    stocks.Add(stock);

                await Task.Delay(500);
            }

            return new AktiepakkeView(kategoriId, currentUsers, pakke.MaxUsers, stocks);
        }

        private Aktiepakke? GetPakke(int kategoriId)
        {
            return kategoriId switch
            {
                1 => new Aktiepakke(
                    1,
                    5,
                    new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN" }
                ),

                2 => new Aktiepakke(
                    2,
                    10,
                    new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC" }
                ),

                3 => new Aktiepakke(
                    3,
                    25,
                    new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC", "IBM", "ORCL", "DIS", "V", "BABA" }
                ),

                _ => null
            };
        }
    }
}