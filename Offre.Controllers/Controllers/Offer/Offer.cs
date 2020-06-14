﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Offre.Controllers.Controllers.Offer
{
    [Authorize]
    [ApiController]
    [Route("offer")]
    public class Offer : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return Ok("Offer ok");
        }
    }
}
