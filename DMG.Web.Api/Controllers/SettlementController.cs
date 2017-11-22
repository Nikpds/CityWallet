using ApiManager;
using ApiManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMG.Web.Api
{
    [Produces("application/json")]
    //[Authorize]
    [Route("api/[controller]")]
    public class SettlementController : Controller
    {
        private readonly ISettlementService _srv;

        public SettlementController(ISettlementService srv)
        {
            _srv = srv;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("")]
        public async Task<IActionResult> InsertSettlement([FromBody] SettlementDto settle)
        {
            try
            {
                if (settle.Bills.Count == 0)
                {
                    return BadRequest("Δεν έχετε επιλέξει λογαριασμούς.");
                }
                if (String.IsNullOrEmpty(settle.SettlementType.Id))
                {
                    return BadRequest("Δεν έχετε επιλέξει τύπο διακανονισμού.");
                }

                var settlement = await _srv.InsertSettlement(settle);

                if (settlement == null)
                {
                    return BadRequest("Δεν ήταν δυνατή η ολοκλήρωση του διακανονισμού.");
                }

                return Ok(settlement);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

        [HttpGet("{settleId}")]
        public async Task<IActionResult> GetSettlement(string settleId)
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var settlement = await _srv.GetUserSettlement(id, settleId);

                if (settlement == null)
                {
                    return NotFound("O Διακανονισμός που ζητήσατε δεν βρέθηκε.");
                }

                return Ok(settlement);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
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
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

        [HttpDelete("{settleId}")]
        public async Task<IActionResult> Delete(string settleId)
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var canceled = await _srv.CancelSettlement(settleId, id);

                if (!canceled)
                {
                    return NotFound("O Διακανονισμός που ζητήσατε δεν βρέθηκε.");
                }

                return Ok(canceled);
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetSettlementTypes()
        {
            try
            {
                var types = await _srv.GetSettlementTypes();

                return Ok(types.OrderByDescending(o => o.Downpayment));
            }
            catch (Exception ex)
            {
                return BadRequest("Συγγνώμη παρουσιάστηκε κάποιο σφάλμα. Ξαναπροσπαθήστε αργότερα.");
            }
        }


    }
}

