using Aktie_loginForm.Models;
using Aktie_loginForm.Services;
using System.Net.Http.Json;

namespace Aktie_loginForm.BusinessLogic
{
    public class AuthBusinessLogic
    {
        private readonly AuthApiService _authService;

        public AuthBusinessLogic(AuthApiService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                return LoginResult.Fail("Email må ikke være tom.");

            if (string.IsNullOrWhiteSpace(password))
                return LoginResult.Fail("Password må ikke være tom.");

            var loginModel = new LoginModel
            {
                Email = email,
                Password = password
            };

            var response = await _authService.Login(loginModel);

            if (!response.IsSuccessStatusCode)
                return LoginResult.Fail("Forkert email eller password.");

            var user = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (user == null)
                return LoginResult.Fail("Kunne ikke læse login svar fra API.");

            return LoginResult.Ok(user);
        }
    }
}