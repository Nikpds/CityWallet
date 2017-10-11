using ApiManager;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
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

        [HttpPost("")]
        public async Task<IActionResult> GetbyId(User entity)
        {
            try
            {
                //var user = new User
                //{
                //    Address = null,
                //    County = null,
                //    Lastname = "perpe",
                //    Name = "nikos",
                //    Vat = "66040",
                //    Password = "1234"
                //};
                var result = await db.UserRepository.Insert(entity);

                db.Save();

                return Ok(result);
            }
            catch
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

                db.Save();

                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await db.UserRepository.GetAll();

                db.Save();

                return Ok(users);
            }
            catch
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
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> ImportUsers()
        {
            try
            {
                var result = UserManager.InsertUsers();

                var completed = db.UserRepository.InsertMany(result.Item1);
                if (completed)
                {
                    db.DebtRepository.InsertMany(result.Item2);
                }
               
                db.Save();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
