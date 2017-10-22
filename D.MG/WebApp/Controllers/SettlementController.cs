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
    public class SettlementController : Controller
    {
        private ISettlementRepository _repo;

        public SettlementController(ISettlementRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetbyId(Settlement entity)
        {
            try
            {
                //var result = await db.SettlementRepository.Insert(entity);

                //db.Save();

                return Ok();
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
                //var settlement = await db.SettlementRepository.GetById(id);

                //db.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //var settlements = await db.SettlementRepository.GetAll();

                //db.Save();

                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}

