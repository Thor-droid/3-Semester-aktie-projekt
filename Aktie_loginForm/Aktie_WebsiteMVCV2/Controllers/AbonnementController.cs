using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AbonnementController : Controller
    {
        [HttpGet]
        public IActionResult Abonnement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(string packageName, string price)
        {
            TempData["SuccessMessage"] =
                $"Køb simuleret: Du har valgt pakken {packageName} til {price}.";

            return RedirectToAction("Abonnement");
        }
    }
}