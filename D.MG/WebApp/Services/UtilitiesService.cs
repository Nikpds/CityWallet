using System.Linq;
using System.Security.Claims;

namespace WebApp.Services
{
    public static class UtilitiesService
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Claims.First(x => x.Type == "Id").Value;
        }
    }
    
}
