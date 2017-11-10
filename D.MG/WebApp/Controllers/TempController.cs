using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMG.Api.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TempController : Controller
    {
        private readonly IUserService _srv;

        public TempController(IUserService srv)
        {
            _srv = srv;
        }

        [HttpGet("")]
        public IActionResult ImAlive()
        {
            return Ok("I am alive");
        }
    }
}
