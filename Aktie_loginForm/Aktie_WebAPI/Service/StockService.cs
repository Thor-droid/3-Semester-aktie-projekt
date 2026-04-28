using Aktie_WebAPI.DTO;
using Aktie_WebsiteMVCV2.Models;
using System.Net.Http.Json;

namespace Aktie_WebAPI.Service
{
    public class StockService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StockService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<GlobalQuote?> GetQuoteAsync(string symbol)
        {
            string apiKey = _config["Finnhub:ApiKey"];
            var url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            // FinnhubQuote
            var finnhub = await response.Content.ReadFromJsonAsync<FinnhubQuote>();

            if (finnhub == null || finnhub.CurrentPrice == 0)
                return null;

            // Convert to your own model
            return new GlobalQuote
            {
                Symbol = symbol.ToUpper(),
                Price = finnhub.CurrentPrice.ToString("F2"),
                ChangePercent = finnhub.ChangePercent.ToString("F2") + "%"
            };
        }
    }
}