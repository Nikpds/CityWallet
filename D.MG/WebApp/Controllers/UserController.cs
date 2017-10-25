using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlContext.Repos;
using System;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _srv;
       
        public UserController(UserService srv)
        {
            _srv = srv;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetbyId()
        {
            try
            {
                var id = User.GetUserId();

                var user = await _srv.GetUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("change/password")]
        public IActionResult ChangePassword(PasswordReset psw)
        {
            try
            {
                //var changed = new User();
                //var user = db.UserRepository.Update(changed);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("counters")]
        public async Task<IActionResult> GetUserCounters()
        {
            try
            {
                var id = User.GetUserId();

                var counters = await _srv.GetUserCounters(id);

                return Ok(counters);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
