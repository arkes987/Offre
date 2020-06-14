using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Offre.Controllers.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class User : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok("User ok");
        }
    }
}
