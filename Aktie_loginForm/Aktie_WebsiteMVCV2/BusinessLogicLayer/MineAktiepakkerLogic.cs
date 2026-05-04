using Aktie_WebsiteMVCV2.DTO.Abonnement;
using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Models.AktiePakkerModels;
using Aktie_WebsiteMVCV2.Services;

namespace Aktie_WebsiteMVCV2.BusinessLogicLayer
{
    public class MineAktiepakkerLogic
    {
        private readonly AbonnementApiService _abonnementService;
        private readonly StockApiService _stockService;

        public MineAktiepakkerLogic(
            AbonnementApiService abonnementService,
            StockApiService stockService)
        {
            _abonnementService = abonnementService;
            _stockService = stockService;
        }

        public async Task<MineAktiepakkerView?> GetMineAktiepakker(int kundeId)
        {
            var abonnement = await _abonnementService.GetByCustomer(kundeId);

            if (abonnement == null)
                return null;

            var symbols = GetSymbols(abonnement.KategoriId);

            var stocks = new List<GlobalQuoteDto>();

            foreach (var symbol in symbols)
            {
                var stock = await _stockService.GetStock(symbol);

                if (stock != null)
                    stocks.Add(stock);

                await Task.Delay(500);
            }

            return new MineAktiepakkerView(abonnement, stocks);
        }

        private List<string> GetSymbols(int kategoriId)
        {
            return kategoriId switch
            {
                1 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN" },
                2 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC" },
                3 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC", "IBM", "ORCL", "DIS", "V", "BABA" },
                _ => new List<string>()
            };
        }
    }
}