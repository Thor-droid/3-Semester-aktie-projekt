using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aktie_WebsiteMVCV2.BusinessLogicLayer;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AbonnementController : Controller
    {
        private readonly AbonnementLogic _abonnementLogic
          ;

        public AbonnementController(AbonnementLogic abonnementLogic)
        {
            _abonnementLogic = abonnementLogic;
        }

        [HttpGet]
        public IActionResult Abonnement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string pakkeId)
        {
            var kundeIdClaim = User.FindFirst("KundeId");

            if (kundeIdClaim == null)
                return RedirectToAction("Login", "Account");

            int kundeId = int.Parse(kundeIdClaim.Value);

            var outcome = await _abonnementLogic.buyAbonnement(pakkeId, kundeId);

            if (outcome.Message == "Success")
            {
                TempData["SuccessMessage"] = $"Du har nu abonneret på {outcome.Aktiepakke} aktiepakken!";
            }
            else
            {
                TempData["Error"] = "Kunne ikke oprette abonnement";
            }

            return RedirectToAction("Index", "Aktiepakker",
             new { kategoriId = outcome.KategoriId });
        }
    }
}