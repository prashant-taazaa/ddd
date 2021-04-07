using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain.Models;

namespace todo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppBaseController : ControllerBase
    {
        //public ApplicationUser _user;
        //private readonly UserManager<ApplicationUser> _userManager;

        public AppBaseController()
        {
            //_userManager = userManager;
            //var context = HttpContext.User.Identity;

        }
    }
}
