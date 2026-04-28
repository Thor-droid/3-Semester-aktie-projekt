using Aktie_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            var stock = await _stockService.GetQuoteAsync(symbol);

            if (stock == null || string.IsNullOrEmpty(stock.Symbol))
                return NotFound("Aktien blev ikke fundet");

            return Ok(stock);
        }
    }
}