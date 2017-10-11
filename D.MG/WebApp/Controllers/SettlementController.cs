using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    public class SettlementController : Controller
    {
        private UnitOfWork db;

        public SettlementController(UnitOfWork u)
        {
            db = u;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetbyId(Settlement entity)
        {
            try
            {
                var result = await db.SettlementRepository.Insert(entity);

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
                var settlement = await db.SettlementRepository.GetById(id);

                db.Save();

                return Ok(settlement);
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
                var settlements = await db.SettlementRepository.GetAll();

                db.Save();

                return Ok(settlements);
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
                var deleted = await db.SettlementRepository.Delete(id);

                db.Save();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpGet("settlements")]
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

