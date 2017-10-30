﻿using ApiManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly PaymentService _srv;

        public PaymentController(PaymentService srv)
        {
            _srv = srv;
        }

        [HttpPost("paybills")]
        public IActionResult Insert([FromBody] List<Bill> entities)
        {
            try
            {
                var id = User.GetUserId();

                if (entities.Count == 0)
                {
                    return BadRequest("Δεν βρέθηκαν λογαριασμοί για πληρωμή");
                }

                var payments = _srv.Payments(entities);

                return Ok(payments);
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

        [HttpGet("payments")]
        public async Task<IActionResult> GetUserPayments()
        {
            try
            {
                var id = User.GetUserId();

                if (id == null || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Σφάλμα ταυτοποίησης χρήστη!");
                }

                var payments = await _srv.GetPayments(id);

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
