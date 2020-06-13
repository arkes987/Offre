using Microsoft.AspNetCore.Mvc;

namespace Offre.Controllers.Controllers.User
{
    [ApiController]
    [Route("user")]
    public class User : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok();
        }
    }
}
