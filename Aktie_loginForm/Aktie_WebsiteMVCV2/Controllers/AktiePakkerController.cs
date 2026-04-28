using Aktie_WebsiteMVCV2.DTO.Abonnement;
using Aktie_WebsiteMVCV2.DTO.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AktiepakkerController : Controller
    {
        private string abonnementApiUrl = "https://localhost:7120/api/abonnement";
        private string stockApiUrl = "https://localhost:7120/api/stock";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var kundeIdClaim = User.FindFirst("KundeId");

            if (kundeIdClaim == null)
                return RedirectToAction("Login", "Account");

            int kundeId = int.Parse(kundeIdClaim.Value);

            using var client = new HttpClient();

            var abonnementResponse = await client.GetAsync(
                $"{abonnementApiUrl}/getByCustomer?kundeId={kundeId}"
            );

            if (!abonnementResponse.IsSuccessStatusCode)
                return RedirectToAction("Abonnement", "Abonnement");

            var abonnement = await abonnementResponse.Content.ReadFromJsonAsync<AbonnementResponse>();

            if (abonnement == null)
                return RedirectToAction("Abonnement", "Abonnement");

            List<string> symbols = abonnement.KategoriId switch
            {
                1 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN" },
                2 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC" },
                3 => new List<string> { "AAPL", "MSFT", "TSLA", "GOOGL", "AMZN", "NVDA", "META", "NFLX", "AMD", "INTC", "IBM", "ORCL", "DIS", "V", "BABA" },
                _ => new List<string>()
            };

            var stocks = new List<GlobalQuoteDto>();

            foreach (var symbol in symbols)
            {
                var stockResponse = await client.GetAsync($"{stockApiUrl}/{symbol}");

                if (stockResponse.IsSuccessStatusCode)
                {
                    var stock = await stockResponse.Content.ReadFromJsonAsync<GlobalQuoteDto>();

                    if (stock != null)
                        stocks.Add(stock);
                }

                await Task.Delay(500);
            }

            return View(stocks);
        }
    }
}