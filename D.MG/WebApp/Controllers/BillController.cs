using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlContext;
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
        private UnitOfWork db;

        public BillController(UnitOfWork u)
        {
            db = u;
        }

        [HttpGet("bills/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            try
            {
                var bills = await db.BillRepository.GetAll(id);

                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
