using System.Configuration;
using System.Net.Http.Json;
using Aktie_loginForm.Model;

namespace Aktie_loginForm.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Login(LoginModel model)
        {
            string url = ConfigurationManager.AppSettings["AuthApiUrl"];

            return await _httpClient.PostAsJsonAsync($"{url}/login", model);
        }

        public async Task<HttpResponseMessage> Register(RegisterViewModel model)
        {
            string url = ConfigurationManager.AppSettings["AuthApiUrl"];

            return await _httpClient.PostAsJsonAsync($"{url}/register", model);
        }
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            string url = ConfigurationManager.AppSettings["AuthApiUrl"];

            var users = await _httpClient.GetFromJsonAsync<List<UserViewModel>>(
                $"{url}/users"
            );

            return users ?? new List<UserViewModel>();
        }
    }
}