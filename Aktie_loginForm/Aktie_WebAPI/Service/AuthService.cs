using Aktie_WebAPI.Models;

public class AuthService
{
    private readonly AuthRepository _repo;

    public AuthService(AuthRepository repo)
    {
        _repo = repo;
    }

    public bool Register(RegisterModel model)
    {
        if (_repo.UserExists(model.Email, model.KundeNavn))
            return false;

        return _repo.CreateUser(model);
    }

    public object Login(LoginModel model)
    {
        var user = _repo.Login(model);

        if (user == null)
            return null;

        return new
        {
            success = true,
            kundeId = user.Value.kundeId,
            navn = user.Value.navn,
            abonnementId = user.Value.abonnementId
        };
    }
}