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
        public IActionResult Register(RegisterModel model)
        {
            bool success = authService.Register(model, out string message);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
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