using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    public class PaymentController : Controller
    {
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

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
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

        [HttpDelete("")]
        public async Task<IActionResult> Delete(string id)
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

        [HttpPut("")]
        public async Task<IActionResult> Delete(Payment payment)
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
