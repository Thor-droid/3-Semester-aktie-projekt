using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceAlertController : ControllerBase
    {
        private readonly PriceAlertRepository _repository;

        public PriceAlertController(PriceAlertRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlert(PriceAlert alert)
        {
            await _repository.AddAlertAsync(alert);

            return Ok("Alert created");
        }
    }
}