using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _srv;

        public UserController(IUserService srv)
        {
            _srv = srv;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetbyId()
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var userDto = await _srv.GetUserWithCounters(id);

                if (userDto == null)
                {
                    return BadRequest("O χρήστης που ζητήσατε δεν υπάρχει.");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

        [HttpPut("change/password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordReset pwd)
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var user = await _srv.GetUser(id);

                if (user == null)
                {
                    return NotFound("Ο xρήστης που ζητήσατε δεν υπάρχει.");
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

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
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
                    return NotFound("Ο σύνδεσμος που ζητήσατε έχει λήξη.");
                }
                else
                {
                    _srv.ChangePassword(user, psw);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

        [HttpGet("counters")]
        public async Task<IActionResult> GetUserCounters()
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var counters = await _srv.GetUserCounters(id);

                return Ok(counters);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
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
                    return NotFound("Ο xρήστης που ζητήσατε δεν υπάρχει.");
                }
                else
                {
                    _srv.SendResetPwdEmail(user);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

    }
}
