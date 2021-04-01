using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Requests;
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskModel createTaskModel)
        {
            var context = HttpContext.User.Identity;

            var user = _unitOfWork.UserRepository.GetByID(createTaskModel.UserId);
            
            var task = user.CreateTask(createTaskModel.Description);

            _unitOfWork.TaskRepository.Add(task);

            await _unitOfWork.CompleteAsync();

            return Created(@$"api/tasks/{task.Id}", task);
        }
    }
}
