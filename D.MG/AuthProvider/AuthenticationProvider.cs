using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;


namespace AuthProvider
{
    public interface IAuthenticationProvider
    {
        SymmetricSecurityKey sigingKey { get; }
        string GetUserId(ClaimsPrincipal user);
        JwtSecurityToken CreateToken(User user, IConfiguration config);

    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        public SymmetricSecurityKey sigingKey { get; }

        public AuthenticationProvider(SymmetricSecurityKey _secretKey)
        {
            sigingKey = _secretKey;
        }

        public JwtSecurityToken CreateToken(User user, IConfiguration config)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("Id", user.Id));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim("Lastname", user.Lastname));

            var key = sigingKey;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              config["Tokens:Issuer"],
              config["Tokens:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: creds);
          
            return token;
        }


        public string GetUserId(ClaimsPrincipal user)
        {
            var isValid = user.HasClaim(x => x.Type == "Id");
            if (isValid)
            {
                return user.Claims.First(x => x.Type == "Id").Value;
            }
            else
            {
                throw new Exception("User account is not valid");
            }
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            var isValid = user.HasClaim(x => x.Type == "Email");
            if (isValid)
            {
                return user.Claims.First(x => x.Type == "Email").Value;
            }
            else
            {
                throw new Exception("User account does not have a valid email");
            }
        }

    }
}
