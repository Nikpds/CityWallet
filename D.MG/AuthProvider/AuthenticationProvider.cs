using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace AuthProvider
{
    public interface IAuthenticationProvider
    {
        bool IsAdmin(ClaimsPrincipal user);
        bool IsManager(ClaimsPrincipal user);
        bool IsPoiManager(ClaimsPrincipal user, string poiId);
        IEnumerable<string> GetManagerPOI(ClaimsPrincipal user);
        string GetUserId(ClaimsPrincipal user);
    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("Administrator");
        }

        public bool IsManager(ClaimsPrincipal user)
        {
            return user.HasClaim(x => x.Type == "PoiId");
        }

        public bool IsPoiManager(ClaimsPrincipal user, string poiId)
        {
            if (IsManager(user))
            {
                return user.Claims.Any(x => x.Type == "PoiId" && x.Value == poiId);
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<string> GetManagerPOI(ClaimsPrincipal user)
        {
            if (IsManager(user))
            {
                var claims = user.Claims.Where(x => x.Type == "PoiId");
                var poiIds = claims.Select(x => x.Value);

                return poiIds;
            }
            else
            {
                return new List<string>();
            }
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
