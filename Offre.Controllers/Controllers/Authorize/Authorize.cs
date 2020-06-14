using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offre.Controllers.Dto.Authorize;
using Offre.Data.Models.Authorize;
using Offre.Services.Interfaces.Authorize;

namespace Offre.Controllers.Controllers.Authorize
{
    [ApiController]
    [Route("authorize")]
    public class Authorize : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        public Authorize(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AuthorizeUser(string login, string password)
        {
            var user = await _authorizeService.TryAuthorizeUser(login, password);

            if (user == null)
            {
                return BadRequest("Invalid login or password.");
            }

            return Ok(ToAuthorizeResponseDto(user));
        }

        private AuthorizeResponseDto ToAuthorizeResponseDto(AuthorizeModel authorizeModel)
        {
            return new AuthorizeResponseDto
            {
                Id = authorizeModel.Id,
                Email = authorizeModel.Email,
                Token = authorizeModel.Token
            };
        }
    }
}
