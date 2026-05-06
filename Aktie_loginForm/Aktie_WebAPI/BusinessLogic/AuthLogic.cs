using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Models;
using Aktie_WebsiteMVCV2.Models;

namespace Aktie_WebAPI.BusinessLogic
{
    public class AuthLogic
    {
        private readonly AuthAccess _authAccess;

        //
        public AuthLogic(AuthAccess authRepository)
        {
            this._authAccess = authRepository;
        }

        public ApiResponse Register(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return ApiResponse.Fail("Email mangler");

            if (string.IsNullOrWhiteSpace(model.KundeNavn))
                return ApiResponse.Fail("Navn mangler");

            if (string.IsNullOrWhiteSpace(model.Password))
                return ApiResponse.Fail("Password mangler");

            if (_authAccess.UserExists(model.Email, model.KundeNavn))
                return ApiResponse.Fail("Bruger findes allerede");

            bool created = _authAccess.CreateUser(model);

            if (created)
            {
                return ApiResponse.Ok("Bruger oprettet");
            }
            else
            {
                return ApiResponse.Fail("Bruger kunne ikke oprettes");
            }
        }

        public ApiResponse DeleteUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ApiResponse.Fail("Email mangler");

            bool deleted = _authAccess.DeleteUserByEmail(email);

            if (deleted)
                return ApiResponse.Ok("Bruger slettet");

            return ApiResponse.Fail("Bruger kunne ikke findes");
        }

        //virtual for at kunne mocke i tests
        public virtual LoginResponse? Login(LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return null;
            }

            return _authAccess.Login(model);
        }

        // UPDATE USER

        public ApiResponse UpdateUser(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return ApiResponse.Fail("Email mangler");

            if (string.IsNullOrWhiteSpace(model.KundeNavn))
                return ApiResponse.Fail("Navn mangler");

            if (string.IsNullOrWhiteSpace(model.Password))
                return ApiResponse.Fail("Password mangler");

            bool updated = _authAccess.UpdateUser(model);

            return updated
                ? ApiResponse.Ok("Bruger opdateret")
                : ApiResponse.Fail("Kunne ikke finde bruger med den email");
        }

        public List<UserViewModel> GetAllUsers()
        {
            return _authAccess.GetAllUsers();
        }
    }
}