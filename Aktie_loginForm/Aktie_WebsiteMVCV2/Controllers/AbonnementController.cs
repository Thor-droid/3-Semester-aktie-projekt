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
        }
    }
}