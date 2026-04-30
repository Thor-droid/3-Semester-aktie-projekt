using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AktiepakkerController : Controller
    {
        private readonly AbonnementApiService _abonnementService;
        private readonly StockApiService _stockService;

        public AktiepakkerController(
            AbonnementApiService abonnementService,
            StockApiService stockService)
        {
            _abonnementService = abonnementService;
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int kategoriId)
        {
            var kundeIdClaim = User.FindFirst("KundeId");

            if (kundeIdClaim == null)
                return RedirectToAction("Login", "Account");

            if (kategoriId < 1 || kategoriId > 3)
                return RedirectToAction("Abonnement", "Abonnement");

            List<string> symbols = kategoriId switch
            {
                1 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN" },
                2 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC" },
                3 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC", "IBM", "ORCL", "DIS", "V", "BABA" },
                _ => new List<string>()
            };

            // hent antal brugere
            int currentUsers = await _abonnementService.GetCurrentUsersByKategoriId(kategoriId);

            // max users, hard coded ind.
            int maxUsers = kategoriId switch
            {
                1 => 5,
                2 => 10,
                3 => 25,
                _ => 0
            };

            var stocks = new List<GlobalQuoteDto>();

            foreach (var symbol in symbols)
            {
                var stock = await _stockService.GetStock(symbol);

                if (stock != null)
                    stocks.Add(stock);

                await Task.Delay(500);
            }

            // send til view
            ViewBag.KategoriId = kategoriId;
            ViewBag.CurrentUsers = currentUsers;
            ViewBag.MaxUsers = maxUsers;
            ViewBag.LedigePladser = maxUsers - currentUsers;

            return View(stocks);
        }
    }
}