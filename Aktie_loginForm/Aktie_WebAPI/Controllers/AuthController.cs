using Microsoft.AspNetCore.Mvc;
using Aktie_WebAPI.Model;
using Aktie_WebAPI.BusinessLogic;
using System.Diagnostics.Eventing.Reader;

namespace Aktie_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthLogic _authLogic;

        // Vi bruger Dependecy injection (kan ses i AuthRespository)
        public AuthController(AuthLogic _authLogic)
        {
            this._authLogic = _authLogic;
        }

        [HttpPost("register")]
        public ActionResult<ApiResponse> Register(RegisterModel model)
        {
            var result = _authLogic.Register(model);

            if (!result.Success)
                return BadRequest(result);

            return result;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(LoginModel model)
        {
            var result = _authLogic.Login(model);

            if (result == null)
                return Unauthorized("Forkert login");

            return result;
        }
        [HttpGet("users")]
        public ActionResult GetAllUsers()
        {
            var users = _authLogic.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{email}")]
        public ActionResult DeleteUserByEmail(string email)
        {
            var result = _authLogic.DeleteUserByEmail(email);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update")]
        public ActionResult UpdateUser(RegisterModel model)
        {
            var result = _authLogic.UpdateUser(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}