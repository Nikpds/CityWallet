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

                var user = await _srv.GetUserWithCounters(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("change/password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordReset pwd)
        {
            try
            {
                var id = User.GetUserId();

                var user = await _srv.GetUser(id);

                if (user == null)
                {
                    return NotFound("User does not exist.");
                }
                else
                {
                    var validated = PasswordHasher.VerifyHashedPassword(user.Password, pwd.OldPassword);

                    if (validated)
                    {
                        _srv.ChangePassword(user, pwd);
                    }
                    else
                    {
                        return BadRequest("Λάθος Κωδικός");
                    }

                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPut("reset/password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordReset psw)
        {
            try
            {

                var user = await _srv.GetByVerification(psw.VerificationToken);

                if (user == null)
                {
                    return NotFound("The link has expired");
                }
                else
                {
                    _srv.ChangePassword(user,psw);
                }

                return Ok("ok");
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
        [AllowAnonymous]
        [HttpGet("requestpasswordreset/{email}")]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            try
            {
                var user = await _srv.GetUserByEmail(email);

                if (user == null)
                {
                    return NotFound("User does not exist.");
                }
                else
                {
                    _srv.SendResetPwdEmail(user);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
