using Aktie_WebsiteMVCV2.Models;
using System.Net.Http.Json;

namespace Aktie_WebsiteMVCV2.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<HttpResponseMessage> Login(LoginModel model)
        {
            string url = _config["ApiSettings:AuthApiUrl"];
            return await _httpClient.PostAsJsonAsync($"{url}/login", model);
        }

        public async Task<HttpResponseMessage> Register(RegisterViewModel model)
        {
            string url = _config["ApiSettings:AuthApiUrl"];
            return await _httpClient.PostAsJsonAsync($"{url}/register", model);
        }
    }
}