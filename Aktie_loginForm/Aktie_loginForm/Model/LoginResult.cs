namespace Aktie_loginForm.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoginResponse? User { get; set; }

        public static LoginResult Ok(LoginResponse user)
        {
            return new LoginResult
            {
                Success = true,
                User = user
            };
        }

        public static LoginResult Fail(string message)
        {
            return new LoginResult
            {
                Success = false,
                Message = message
            };
        }
    }
}