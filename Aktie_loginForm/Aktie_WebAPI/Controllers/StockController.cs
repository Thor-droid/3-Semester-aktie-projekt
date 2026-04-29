using Aktie_WebAPI.Service;
using Aktie_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService stockService;

        public StockController(StockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<GlobalQuote>> Get(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return BadRequest("Symbol mangler");

            var stock = await stockService.GetQuoteAsync(symbol);

            if (stock == null || string.IsNullOrEmpty(stock.Symbol))
                return NotFound();

            return stock;
        }
    }
}