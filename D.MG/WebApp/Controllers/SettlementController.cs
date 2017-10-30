using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class SettlementController : Controller
    {
        private readonly SettlementService _srv;

        public SettlementController(SettlementService srv)
        {
            _srv = srv;
        }

        [HttpPost("")]
        public async Task<IActionResult> InsertSettlement([FromBody] Settlement settle)
        {
            try
            {
                var settlement = await _srv.InsertSettlement(settle);

                return Ok(settlement);
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
                var settlement = await _srv.GetUserSettlements(id);

                return Ok(settlement);
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
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var settlements = await _srv.GetUserSettlements(id);

                return Ok(settlements);
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
                var canceled = _srv.CancelSettlement(id);

                return Ok(canceled);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetSettlementTypes()
        {
            try
            {
                var types = await _srv.GetSettlementTypes();

                return Ok(types);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}

