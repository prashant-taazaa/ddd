using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Requests;
using todo.infrastructure.persistence;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Controllers
{
    public class TasksController : AppBaseController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        public TasksController(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Create a new task for logged in user
        /// </summary>
        /// <param name="createTaskModel"> Task Model With Description which needs to be created</param>
        /// <returns>New Created Task</returns>
        /// <response code="201">Returns the newly created task</response>
        /// <response code="404">If the user not found</response>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskModel createTaskModel)
        {

            var context = HttpContext.User.Identity;

            var user = _userRepository.GetByID(createTaskModel.UserId);

            if (user == null)
            {
                return NotFound($"User not found with id {createTaskModel.UserId}");
            }

            var task = user.CreateTask(createTaskModel.Description);

            _taskRepository.Add(task);

            await _taskRepository.SaveChangesAsync();

            return Created(@$"api/tasks/{task.Id}", task);

        }


        /// <summary>
        /// Return All task of logged in user
        /// </summary>
        /// <param name="userId">User Id of logged in user</param>
        /// <returns>List of task of logged in User </returns>
        /// <response code="200">List of task of logged in User</response>
        /// <response code="404">If the user not found</response>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTasksAsync([FromQuery] Guid userId)
        {

            var context = HttpContext.User.Identity;

            var user = _userRepository.GetByID(userId);
            if (user == null)
            {
                return NotFound($"User not found with id {userId}");
            }
            else
            {
                var tasks = _taskRepository.GetAll().Where(x => x.CreatedBy == user);
              
                return Ok(tasks);
            }

        }
    }
}
