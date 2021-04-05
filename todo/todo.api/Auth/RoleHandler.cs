using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace todo.api.Auth
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
      

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var authorizationFilterContext = context.Resource as AuthorizationFilterContext;

            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value.Equals(requirement.Role)))
            {
                context.Succeed(requirement);
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                _httpContextAccessor.HttpContext.Response.WriteAsync("Unauthorize Access");
            }


            return Task.CompletedTask;
        }


    }
}
