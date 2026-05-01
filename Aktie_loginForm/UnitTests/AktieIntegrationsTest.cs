using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using Aktie_WebAPI.Service;
using Aktie_WebsiteMVCV2.Models;

namespace UnitTests
{
    public class AktieIntegrationsTest
    {
        [Fact]
        public async Task GetQuoteAsync_ReturnsData_FromFinnhub()
        {
            // Arrange
            using var httpClient = new HttpClient();

            Environment.SetEnvironmentVariable(
                "Finnhub__ApiKey",
                "d7nkb4hr01qppri56j50d7nkb4hr01qppri56j5g"
            );

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var service = new StockService(httpClient, config);

            // Act
            var result = await service.GetQuoteAsync("AAPL");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AAPL", result.Symbol);
            Assert.False(string.IsNullOrEmpty(result.Price));
            Assert.False(string.IsNullOrEmpty(result.ChangePercent));
        }
    }
}