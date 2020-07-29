using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offre.Abstraction.Dto.Authenticate;
using Offre.Logic.Interfaces.Authenticate;

namespace Offre.Controllers.Controllers.Authorize
{
    [ApiController]
    [Route("authorize")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateLogic _authorizeLogic;

        public AuthenticateController(IAuthenticateLogic authorizeLogic)
        {
            _authorizeLogic = authorizeLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthenticateResponseDto>> AuthorizeUser(string login, string password)
        {
            var user = await _authorizeLogic.TryAuthUser(login, password);

            if (user == null)
            {
                return BadRequest("Invalid login or password.");
            }

            return Ok(user);
        }
    }
}
