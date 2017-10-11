using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    public class PaymentController : Controller
    {
        private UnitOfWork db;

        public PaymentController(UnitOfWork u)
        {
            db = u;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetbyId(Payment entity)
        {
            try
            {
                var result = await db.PaymentRepository.Insert(entity);

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
                var payment = await db.PaymentRepository.GetById(id);

                db.Save();

                return Ok(payment);
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
                var payments = await db.PaymentRepository.GetAll();

                db.Save();

                return Ok(payments);
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
                var deleted = await db.PaymentRepository.Delete(id);

                db.Save();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("payments")]
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
