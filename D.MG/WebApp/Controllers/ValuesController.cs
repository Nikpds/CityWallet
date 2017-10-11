﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SqlContext.Repos;
using SqlContext;
using Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private UnitOfWork unitOfWork;

        public ValuesController(UnitOfWork u)
        {
            unitOfWork = u;
        }

        // GET api/values
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.unitOfWork != null)
                {
                    this.unitOfWork.Dispose();
                    this.unitOfWork = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}