using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Aktie_WebAPI;
using Aktie_WebsiteMVCV2.Models;

namespace UnitTests
{
    public class SøgEfterAktie
    {
        [Fact]
        public async Task GetQuoteAsync_ReturnererAktieData_NaarApiSvarErOK()
        {
            string json = "{ \"Global Quote\": { \"01. symbol\": \"AAPL\", \"05. price\": \"150.00\", \"10. change percent\": \"1.25%\" } }";

            var handler = new FakeHttpMessageHandler(json, HttpStatusCode.OK);
            var httpClient = new HttpClient(handler);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["AlphaVantage:ApiKey"]).Returns("TESTKEY");

            var service = new StockService(httpClient, configMock.Object);

            var result = await service.GetQuoteAsync("AAPL");

            Assert.NotNull(result);
            Assert.NotNull(result.GlobalQuote);
            Assert.Equal("AAPL", result.GlobalQuote.Symbol);
            Assert.Equal("150.00", result.GlobalQuote.Price);
            Assert.Equal("1.25%", result.GlobalQuote.ChangePercent);
        }

        [Fact]
        public async Task GetQuoteAsync_ReturnererNull_NaarApiSvarFejler()
        {
            var handler = new FakeHttpMessageHandler("", HttpStatusCode.BadRequest);
            var httpClient = new HttpClient(handler);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["AlphaVantage:ApiKey"]).Returns("TESTKEY");

            var service = new StockService(httpClient, configMock.Object);

            var result = await service.GetQuoteAsync("AAPL");

            Assert.Null(result);
        }
    }

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseBody;
        private readonly HttpStatusCode _statusCode;

        public FakeHttpMessageHandler(string responseBody, HttpStatusCode statusCode)
        {
            _responseBody = responseBody;
            _statusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_responseBody)
            };

            return Task.FromResult(response);
        }
    }
}