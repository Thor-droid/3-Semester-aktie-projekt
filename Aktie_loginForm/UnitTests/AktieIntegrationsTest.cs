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

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("AlphaVantage:ApiKey", "1U6ZNQU1PDOMSIXB")
                })
                .Build();

            var service = new StockService(httpClient, config);

            // Act
            var result = await service.GetQuoteAsync("AAPL");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.GlobalQuote);

            Assert.Equal("AAPL", result.GlobalQuote.Symbol);

            // pris kan ikke være fast, fordi den ændrer sig hele tiden
            Assert.False(string.IsNullOrEmpty(result.GlobalQuote.Price));
        }
    }
}