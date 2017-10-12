using AuthProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SqlContext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private UnitOfWork _db;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public AuthController(UnitOfWork db, IConfiguration config, IAuthenticationProvider auth)
        {
            _db = db;
            _config = config;
            _key = auth.sigingKey;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbUser = await _db.UserRepository.GetByUsername(model.Username);
                
                if (dbUser != null && PasswordHasher.VerifyHashedPassword(dbUser.Password, model.Password))
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim("Id", dbUser.Id));
                    claims.Add(new Claim("Name", dbUser.Name));

                    var key = _key;
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                      _config["Tokens:Issuer"],
                      claims,
                      expires: DateTime.Now.AddDays(30),
                      signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }

            }
            return BadRequest("Could not create token");
        }
    }
}