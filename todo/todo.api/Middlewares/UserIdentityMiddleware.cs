using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Extensions;
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Middlewares
{
    public class UserIdentityMiddleware
    {
        private readonly RequestDelegate _next;
        public UserIdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var sid = context.GetUserSid();

            if (Guid.Empty != sid)
            {
                context.Items.Add("currentUser", userRepository.GetByID(sid));
            }

            return _next(context);
        } 
    }
}
