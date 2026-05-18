using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/abonnementer")]
    public class AbonnementController : ControllerBase
    {
        private readonly AbonnementLogic service;

        public AbonnementController(AbonnementLogic service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult<ApiResponse> Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            bool success = service.Subscribe(kundeId, kategoriId, aktiepakkeId);

            if (!success)
            {
                return BadRequest(ApiResponse.Fail("Du abonnerer allerede på denne aktiepakke, eller der er ingen pladser tilbage."));
            }

            return ApiResponse.Ok("Du er nu tilmeldt aktiepakken!");
        }

        [HttpGet("kunde/{kundeId}")]
        public ActionResult<AbonnementResponse> GetByCustomer(int kundeId)
        {
            var kategoriId = service.GetKategoriByCustomer(kundeId);

            if (kategoriId == null)
                return NotFound();

            return new AbonnementResponse(kategoriId.Value);
        }

        [HttpGet("kategori/{kategoriId}/antal")]
        public IActionResult CountByKategori(int kategoriId)
        {
            int count = service.CountByKategori(kategoriId);
            return Ok(count);
        }
    }
}