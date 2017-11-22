using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMG.Web.Api
{
    [Produces("application/json")]
    //[Authorize]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private readonly IBillService _srv;

        public BillController(IBillService srv)
        {
            _srv = srv;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
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
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
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
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }
    }
}
