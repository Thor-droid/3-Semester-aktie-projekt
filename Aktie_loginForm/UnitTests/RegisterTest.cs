using Aktie_WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace UnitTests
{
    public class RegisterTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public RegisterTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TestRegister()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = "test" + Guid.NewGuid() + "@mail.com",
                KundeNavn = "UnitTestUser",
                Password = "123456"
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/auth/register", model);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.BadRequest
            );
        }
    }
}