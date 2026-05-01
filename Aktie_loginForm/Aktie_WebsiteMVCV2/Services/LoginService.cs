    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Security.Claims;
    

    public class LoginService : ILoginService
    {
        public async Task SignInUser(HttpContext httpContext, LoginResponse result)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Navn),
                new Claim("KundeId", result.KundeId.ToString()),
                new Claim("IsAdmin", result.IsAdmin.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
        }

        public async Task SignOutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }