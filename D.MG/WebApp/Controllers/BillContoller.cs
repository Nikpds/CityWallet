using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System;
using System.Collections.Generic;
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

        [HttpPost("")]
        public async Task<IActionResult> Insert(Bill entity)
        {
            try
            {
                var result = await db.BillRepository.Insert(entity);

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
                var debt = await db.BillRepository.GetById(id);

                return Ok(debt);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("bills/{userId}")]
        public async Task<IActionResult> GetAll(string userId)
        {
            try
            {
                var debts = await db.BillRepository.GetAll(userId);

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
                var deleted = await db.BillRepository.Delete(id);

                db.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("paybills")]
        public async Task<IActionResult> Insert([FromBody] List<Bill> entities)
        {
            try
            {
                var payments = BillManager.PayBills(entities);
                var result = db.BillRepository.UpdateMany(payments);

                db.Save();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
