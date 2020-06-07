using Microsoft.AspNetCore.Mvc;

namespace Offre.Controllers.Controllers.UserAccount
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccount : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok();
        }
    }
}
