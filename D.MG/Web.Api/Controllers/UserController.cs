using ApiManager;
using Microsoft.AspNetCore.Mvc;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private UserRepository userRepo;

        public UserController(UserRepository repo)
        {
            userRepo = repo;
        }

        [HttpGet("")]
        public string Get(int id)
        {
            UserManager.InsertUsers();
            return "value";
        }

    }
}
