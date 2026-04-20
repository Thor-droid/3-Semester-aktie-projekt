using Aktie_WebAPI.Models;
using Aktie_WebsiteMVCV2.Models;
using System.Net.Http.Json;

namespace Aktie_WebAPI
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

        public async Task<StockQuoteResponse?> GetQuoteAsync(string symbol)
        {
            string apiKey = _config["AlphaVantage:ApiKey"];
            var url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<StockQuoteResponse>();
            return result;
        }
    }
}