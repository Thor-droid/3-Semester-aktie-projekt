using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AbonnementController : Controller
    {
        private string apiUrl = "https://localhost:7120/api/abonnement";

        [HttpGet]
        public IActionResult Abonnement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string packageName)
        {
            using var client = new HttpClient();

            var kundeIdClaim = User.FindFirst("KundeId");

            if (kundeIdClaim == null)
                return RedirectToAction("Login", "Account");

            int kundeId = int.Parse(kundeIdClaim.Value);

            int kategoriId = packageName switch
            {
                "Basis" => 1,
                "Pro" => 2,
                "Premium" => 3,
                _ => 1
            };

            int aktiepakkeId = packageName switch
            {
                "Basis" => 1,
                "Pro" => 2,
                "Premium" => 3,
                _ => 1
            };

            var response = await client.PostAsync(
                $"{apiUrl}/subscribe?kundeId={kundeId}&kategoriId={kategoriId}&aktiepakkeId={aktiepakkeId}",
                null
            );

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"Du har valgt {packageName}!";
                return RedirectToAction("Index", "Aktiepakker");
            }

            TempData["Error"] = "Kunne ikke oprette abonnement";
            return RedirectToAction("Abonnement");
        }
    }
  }