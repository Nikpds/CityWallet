using ApiManager;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(string id)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll ()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> Delete(User user)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> ImportUsers(User user)
        {
            try
            {
                UserManager.InsertUsers();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
