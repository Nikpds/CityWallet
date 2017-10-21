using AuthProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private UnitOfWork db;

        public UserController(UnitOfWork u)
        {
            db = u;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(string id)
        {
            try
            {
                var user = await db.UserRepository.GetById(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("change/password")]
        public IActionResult GetbyId(PasswordReset psw)
        {
            try
            {
                var changed = new User();
                var user = db.UserRepository.Update(changed);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await db.UserRepository.GetAll();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
