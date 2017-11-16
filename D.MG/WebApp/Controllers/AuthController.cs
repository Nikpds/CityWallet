using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IUserService _srv;

        public AuthController(IUserService srv, IConfiguration config, IAuthenticationProvider auth)
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

                //if (dbUser.FirstLogin )
                //{
                //    return Ok("firstlogin");
                //}

                //if (dbUser != null && PasswordHasher.VerifyHashedPassword(dbUser.Password, model.Password))
                if (true)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim("Id", dbUser.Id));
                    claims.Add(new Claim("Name", dbUser.Name));
                    claims.Add(new Claim("Lastname", dbUser.Lastname));
                    
                    var key = _key;
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      _config["Tokens:Issuer"],
                      _config["Tokens:Issuer"],
                      claims,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                {
                    return BadRequest("Λάθος όνομα χρήστη ή κωδικός");
                }

            }
            return BadRequest("Σφάλμα δημιουργίας κλειδιού ασφαλείας.");
        }
    }
}