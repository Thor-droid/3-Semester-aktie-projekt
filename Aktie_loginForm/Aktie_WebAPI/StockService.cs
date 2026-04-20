using Aktie_WebAPI.Model;

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

        public async Task<List<Stock>> GetStocksAsync()
        {
            string apiKey = _config["AlphaVantage:ApiKey"];
            var url = $"https://www.alphavantage.co/query?function=LISTING_STATUS&apikey={apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<Stock>();

            var csv = await response.Content.ReadAsStringAsync();
            var lines = csv.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var stocks = new List<Stock>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Trim().Split(',');

                if (values.Length >= 7)
                {
                    stocks.Add(new Stock
                    {
                        Symbol = values[0],
                        Name = values[1],
                        Exchange = values[2],
                        AssetType = values[3],
                        IpoDate = values[4],
                        DelistingDate = values[5],
                        Status = values[6]
                    });
                }
            }

            return stocks;
        }
    }
}