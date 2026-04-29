using Aktie_WebsiteMVCV2.DTO.Stock;
using System.Net.Http.Json;

namespace Aktie_WebsiteMVCV2.Services
{
    public class StockApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StockApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<GlobalQuoteDto?> GetStock(string symbol)
        {
            string url = _config["ApiSettings:StockApiUrl"];

            var response = await _httpClient.GetAsync($"{url}/{symbol}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<GlobalQuoteDto>();
        }
    }
}