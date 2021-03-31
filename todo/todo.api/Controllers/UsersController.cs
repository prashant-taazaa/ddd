using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Requests;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel createUserModel)
        {
            var user = new User(createUserModel.Email);
            _unitOfWork.UserRepository.Add(user);

            await _unitOfWork.CompleteAsync();

            return Created(@$"api/users/{user.Email}", user);
        }
    }
}
