using Microsoft.AspNetCore.Mvc;

namespace Offre.Controllers.Controllers.Offer
{
    [ApiController]
    [Route("[controller]")]
    public class Offer : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok();
        }
    }
}
