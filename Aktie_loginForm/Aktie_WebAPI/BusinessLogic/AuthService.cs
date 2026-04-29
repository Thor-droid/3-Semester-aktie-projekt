using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Models;

namespace Aktie_WebAPI.BusinessLogic
{
    public class AuthService
    {
        private readonly AuthRepository authRepository;

        //
        public AuthService(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public ApiResponse Register(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return ApiResponse.Fail("Email mangler");

            if (string.IsNullOrWhiteSpace(model.KundeNavn))
                return ApiResponse.Fail("Navn mangler");

            if (string.IsNullOrWhiteSpace(model.Password))
                return ApiResponse.Fail("Password mangler");

            if (authRepository.UserExists(model.Email, model.KundeNavn))
                return ApiResponse.Fail("Bruger findes allerede");

            bool created = authRepository.CreateUser(model);

            if (created)
            {
                return ApiResponse.Ok("Bruger oprettet");
            }
            else
            {
                return ApiResponse.Fail("Bruger kunne ikke oprettes");
            }
        }

        public LoginResponse? Login(LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return null;
            }

            return authRepository.Login(model);
        }
    }
}