using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Aktie_WebAPI;

namespace UnitTests
{
    public class AktieIntegrationsTest
    {
        [Fact]
        public async Task GetQuoteAsync_ReturnsData_FromRealApi()
        {
            // Arrange
            var httpClient = new HttpClient();

            Environment.SetEnvironmentVariable("AlphaVantage__ApiKey", "1U6ZNQU1PDOMSIXB");

            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

            var service = new StockService(httpClient, config);

            
            var result = await service.GetQuoteAsync("AAPL");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.GlobalQuote);

            Assert.Equal("AAPL", result.GlobalQuote.Symbol);

            // prisen ændrer sig hele tiden, så vi tjekker bare at den ikke er null eller tom
            Assert.False(string.IsNullOrEmpty(result.GlobalQuote.Price));
        }
    }
}