public interface ILoginService
{
    Task SignInUser(HttpContext httpContext, LoginResponse result);
    Task SignOutUser(HttpContext httpContext);
}