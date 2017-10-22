using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserManager _manager;
        private IUserRepository _repo;

        public UserController(UserManager mng, IUserRepository repo)
        {
            _manager = mng;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(string id)
        {
            try
            {
                var user = await _manager.GetUser(id);
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


    }
}
