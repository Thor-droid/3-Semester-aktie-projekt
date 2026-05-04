using Aktie_WebsiteMVCV2.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{
    [Authorize]
    public class AktiepakkerController : Controller
    {
        private readonly AktiepakkeLogic _aktiepakkeLogic;

        public AktiepakkerController(AktiepakkeLogic aktiepakkeLogic)
        {
            _aktiepakkeLogic = aktiepakkeLogic;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int kategoriId)
        {
            var model = await _aktiepakkeLogic.GetAktiepakke(kategoriId);

            if (model == null)
                return RedirectToAction("Abonnement", "Abonnement");

            return View(model);
        }
    }
}