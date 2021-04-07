using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace todo.api.Extensions
{
    public static class Extensions
    {
        public static Guid GetUserSid(this HttpContext context)
        {
            var sid= context.User.Claims
                .Where(x => x.Type == ClaimTypes.Sid)
                .Select(x => x.Value)
                .FirstOrDefault();

            return Guid.Parse(sid);
        }
    }
}
