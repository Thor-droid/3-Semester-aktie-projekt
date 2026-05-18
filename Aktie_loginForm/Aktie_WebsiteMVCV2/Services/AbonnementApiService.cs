using Aktie_WebsiteMVCV2.DTO.Abonnement;
using System.Net.Http.Json;

namespace Aktie_WebsiteMVCV2.Services
{
    public class AbonnementApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AbonnementApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<HttpResponseMessage> Subscribe(int kundeId, int kategoriId, int aktiepakkeId)
        {
            string url = _config["ApiSettings:AbonnementApiUrl"];

            return await _httpClient.PostAsync(
                $"{url}?kundeId={kundeId}&kategoriId={kategoriId}&aktiepakkeId={aktiepakkeId}",
                null
            );
        }

        public async Task<AbonnementResponse?> GetByCustomer(int kundeId)
        {
            string url = _config["ApiSettings:AbonnementApiUrl"];

            var response = await _httpClient.GetAsync(
                $"{url}/kunde/{kundeId}"
            );

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AbonnementResponse>();
        }

        public async Task<int> GetCurrentUsersByKategoriId(int kategoriId)
        {
            string url = _config["ApiSettings:AbonnementApiUrl"];

            var response = await _httpClient.GetAsync(
                $"{url}/kategori/{kategoriId}/antal"
            );

            if (!response.IsSuccessStatusCode)
                return 0;

            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}