using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class DebtController : Controller
    {
        private UnitOfWork db;

        public DebtController(UnitOfWork u)
        {
            db = u;
        }

        [HttpPost("")]
        public async Task<IActionResult> Insert(Debt entity)
        {
            try
            {
                var result = await db.DebtRepository.Insert(entity);

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
                var debt = await db.DebtRepository.GetById(id);

                return Ok(debt);
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
                var debts = db.DebtRepository.GetAll("005fd3fa-6e3a-4420-a178-e972b90a6c61");

                return Ok(debts);
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
                var deleted = await db.DebtRepository.Delete(id);

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
