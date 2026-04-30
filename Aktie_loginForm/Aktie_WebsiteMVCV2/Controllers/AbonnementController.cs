using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AbonnementController : Controller
    {
        private readonly AbonnementApiService _abonnementApiService;

        public AbonnementController(AbonnementApiService abonnementApiService)
        {
            _abonnementApiService = abonnementApiService;
        }

        [HttpGet]
        public IActionResult Abonnement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string packageName)
        {
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

            int aktiepakkeId = kategoriId;

            var response = await _abonnementApiService.Subscribe(kundeId, kategoriId, aktiepakkeId);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"Du har nu abonneret på {packageName} aktiepakken!";
                return RedirectToAction("Index", "Aktiepakker", new { kategoriId = kategoriId });
            }

            TempData["Error"] = "Kunne ikke oprette abonnement";
            return RedirectToAction("Index", "Aktiepakker", new { kategoriId = kategoriId });
        }
    }
}