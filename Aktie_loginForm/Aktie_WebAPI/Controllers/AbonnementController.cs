using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/abonnement")]
    public class AbonnementController : ControllerBase
    {
        private readonly AbonnementService service;

        // 
        public AbonnementController(AbonnementService service)
        {
            this.service = service;
        }

        [HttpPost("subscribe")]
        public ActionResult<ApiResponse> Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            bool success = service.Subscribe(kundeId, kategoriId, aktiepakkeId);

            if (!success)
                return BadRequest(ApiResponse.Fail("Ingen pladser tilbage eller fejl"));

            return ApiResponse.Ok("Tilmeldt!");
        }

        [HttpGet("getByCustomer")]
        public ActionResult<AbonnementResponse> GetByCustomer(int kundeId)
        {
            var kategoriId = service.GetKategoriByCustomer(kundeId);

            if (kategoriId == null)
                return NotFound();

            return new AbonnementResponse(kategoriId.Value);
        }
    }
}