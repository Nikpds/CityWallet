using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System;
using System.Threading.Tasks;

namespace WebApp.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private UnitOfWork db;

        public UserController(UnitOfWork u)
        {
            db = u;
        }

        [HttpGet("new")]
        public async Task<IActionResult> Insert(User entity)
        {
            try
            {
                var result = await db.UserRepository.Insert(entity);

                db.Save();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var deleted = await db.UserRepository.Delete(id);

                db.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
