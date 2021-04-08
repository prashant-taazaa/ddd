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
        private readonly IUserRepository _userRepository;
        public UserIdentityMiddleware(RequestDelegate next, IUserRepository userRepository)
        {
            _next = next;
            _userRepository = userRepository;
        }

        public Task Invoke(HttpContext context)
        {
            var sid = context.GetUserSid();

            if (Guid.Empty != sid)
            {
                context.Items.Add("currentUser", _userRepository.GetByID(sid));
            }

            return _next(context);
        } 
    }
}
