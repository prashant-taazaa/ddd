﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Auth
{
    public class RoleRequirement: IAuthorizationRequirement
    {
        public string Role { get;  }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
