using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DebtController : Controller
    {
        private UnitOfWork db;

        public DebtController(UnitOfWork u)
        {
            db = u;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetbyId(Debt entity)
        {
            try
            {
                var result = await db.DebtRepository.Insert(entity);

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
                var debt = await db.DebtRepository.GetById(id);

                db.Save();

                return Ok(debt);
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
                var debts = await db.DebtRepository.GetAll();

                db.Save();

                return Ok(debts);
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
                var deleted = await db.DebtRepository.Delete(id);

                db.Save();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("debts")]
        public async Task<IActionResult> ImportUsers()
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
    }
}
