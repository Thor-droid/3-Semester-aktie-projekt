using Microsoft.AspNetCore.Mvc;
using Aktie_WebAPI.Models;
using Aktie_WebAPI.BusinessLogic;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthLogic authService;

        // Vi bruger Dependecy injection (kan ses i AuthRespository)
        public AuthController(AuthLogic authService)
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

        [HttpDelete("delete/{email}")]
        public ActionResult DeleteUserByEmail(string email)
        {
            var result = authService.DeleteUserByEmail(email);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}