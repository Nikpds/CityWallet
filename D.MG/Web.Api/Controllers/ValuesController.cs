﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SqlContext.Repos;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private UserRepository userRepo;

        public ValuesController(UserRepository repo)
        {
            userRepo = repo;
        }
        // GET api/values
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

        [HttpGet("jwt/{id}")]
        [Authorize]
        public string GetJWT(int id)
        {
            return "visit by jwt auth";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
