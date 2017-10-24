using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
