using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{

    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private IBillRepository _repo;

        public BillController(IBillRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("bills/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            try
            {
                var bills = await _repo.GetAll(id);
                

                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
