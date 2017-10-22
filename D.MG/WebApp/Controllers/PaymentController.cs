using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private IPaymentRepository _repo;

        public PaymentController(IPaymentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(string id)
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

        [HttpGet("payments/{id}")]
        public async Task<IActionResult> GetAll(string id)
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

        [HttpPost("paybills")]
        public async Task<IActionResult> Insert([FromBody] List<Bill> entities)
        {
            try
            {
                //var payments = BillManager.Payments(entities);
                //var result = db.PaymentRepository.UpdateMany(payments);

                //db.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("creditcard")]
        public IActionResult ValidateCreditCard([FromBody] CreditCard card)
        {
            try
            {
                Thread.Sleep(2000);  //Simulating validation of  credit card with 2 sec delay
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
