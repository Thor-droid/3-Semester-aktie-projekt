using Aktie_WebsiteMVCV2.Models;
using Aktie_WebsiteMVCV2.Services;

namespace Aktie_WebsiteMVCV2.BusinessLogicLayer
{
    public class AccountLogic
    {
        private readonly AuthApiService _authService;

        public AccountLogic(AuthApiService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponse?> Login(LoginModel model)
        {
            var response = await _authService.Login(model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var response = await _authService.Register(model);

            return response.IsSuccessStatusCode;
        }
    }
}