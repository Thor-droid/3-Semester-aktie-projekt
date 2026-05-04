using Aktie_WebsiteMVCV2.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{

    // Controlleren her beskriver kundens egne aktiepakker det er altså siden efter man har klikket køb
    [Authorize]
    public class MineAktiepakkerController : Controller
    {
        private readonly MineAktiepakkerLogic _mineAktiepakkerLogic;

        public MineAktiepakkerController(MineAktiepakkerLogic mineAktiepakkerLogic)
        {
            _mineAktiepakkerLogic = mineAktiepakkerLogic;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var kundeIdClaim = User.FindFirst("KundeId");

            if (kundeIdClaim == null)
                return RedirectToAction("Login", "Account");

            int kundeId = int.Parse(kundeIdClaim.Value);

            var model = await _mineAktiepakkerLogic.GetMineAktiepakker(kundeId);

            return View(model);
        }
    }
}