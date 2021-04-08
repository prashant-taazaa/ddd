using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Extensions;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppBaseController : ControllerBase
    {
        public User _user;
        public Guid _userId;
        public AppBaseController(IUserRepository userRepository)
        {
            var sid = HttpContext.GetUserSid();
            _userId = sid;
            _user = userRepository.GetByID(sid);

        }
    }
}
