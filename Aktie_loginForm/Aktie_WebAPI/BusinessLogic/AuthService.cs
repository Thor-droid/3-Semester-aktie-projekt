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

        public bool Register(RegisterModel model, out string message)
        {
            if (authRepository.UserExists(model.Email, model.KundeNavn))
            {
                message = "Bruger findes allerede";
                return false;
            }

            bool created = authRepository.CreateUser(model);

            message = created ? "Bruger oprettet" : "Bruger kunne ikke oprettes";
            return created;
        }

        public LoginResponse? Login(LoginModel model)
        {
            return authRepository.Login(model);
        }
    }
}