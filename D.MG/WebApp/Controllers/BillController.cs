using ApiManager;
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
        private readonly BillService _srv;

        public BillController(BillService srv)
        {
            _srv = srv;
        }

        [HttpGet("bills/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            try
            {
                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }
                
                var bills = await _srv.GetUnpaidBills(id);
                
                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
