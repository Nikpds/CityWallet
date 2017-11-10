using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{

    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private readonly IBillService _srv;

        public BillController(IBillService srv)
        {
            _srv = srv;
        }

        [HttpGet("bills")]
        public async Task<IActionResult> GetUnpaidBills()
        {
            try
            {
                var id = User.GetUserId();

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

        [HttpGet("bills/paid")]
        public async Task<IActionResult> GetPaidBills()
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var bills = await _srv.GetPaidBills(id);

                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("bills/onsettlement")]
        public async Task<IActionResult> GetBillsOnSettlement()
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var bills = await _srv.GetBillsOnSettlement(id);

                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
