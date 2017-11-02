using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserService _srv;
        public AuthController(UserService srv, IConfiguration config, IAuthenticationProvider auth)
        {
            _srv = srv;
            _config = config;
            _key = auth.sigingKey;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbUser = await _srv.GetByUsername(model.Username);

                if (dbUser != null && PasswordHasher.VerifyHashedPassword(dbUser.Password, model.Password))
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim("Id", dbUser.Id));
                    claims.Add(new Claim("Name", dbUser.Name));
                    claims.Add(new Claim("Lastname", dbUser.Lastname));


                    var key = _key;
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                      _config["Tokens:Issuer"],
                      claims,
                      expires: DateTime.Now.AddMinutes(300),
                      signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }
                else
                {
                    return Ok();
                }

            }
            return BadRequest("Could not create token");
        }
    }
}