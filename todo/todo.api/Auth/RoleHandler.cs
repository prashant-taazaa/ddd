using Microsoft.AspNetCore.Authorization;
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
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type==ClaimTypes.Role && c.Value.Equals(requirement.Role)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }


    }
}
