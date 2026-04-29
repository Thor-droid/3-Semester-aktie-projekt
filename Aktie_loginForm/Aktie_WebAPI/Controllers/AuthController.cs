using Microsoft.AspNetCore.Mvc;
using Aktie_WebAPI.Models;
using Aktie_WebAPI.BusinessLogic;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        // Use constructor injection to obtain AuthService (so DI can provide its required AuthRepository)
        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<ApiResponse> Register(RegisterModel model)
        {
            var result = authService.Register(model);

            if (!result.Success)
                return BadRequest(result);

            return result; 
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(LoginModel model)
        {
            var result = authService.Login(model);

            if (result == null)
                return Unauthorized("Forkert login");

            return result;
        }
    }
}