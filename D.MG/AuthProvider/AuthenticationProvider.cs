using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace AuthProvider
{
    public interface IAuthenticationProvider
    {
        SymmetricSecurityKey sigingKey { get; }
        string GetUserId(ClaimsPrincipal user);
        bool IsFirstLogin(ClaimsPrincipal user);
    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        public SymmetricSecurityKey sigingKey { get; }

        public AuthenticationProvider(SymmetricSecurityKey _secretKey)
        {
            this.sigingKey = _secretKey;
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

        public bool IsFirstLogin(ClaimsPrincipal user)
        {
            return false;
        }
        
    }
}
