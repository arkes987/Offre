using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Offre.Services.Interfaces.Authorize;
using Offre.Validation.AuthorizePrefilters;
using System;

namespace Offre.Controllers.Controllers.Authorize
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult AuthorizeUser(string login, string password)
        {
            var user = _authorizeService.TryAuthorizeUser(login, password);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
