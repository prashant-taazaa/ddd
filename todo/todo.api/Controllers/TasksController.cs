using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Requests;
using todo.api.Extensions;
using todo.domain.Enums;
using todo.domain.Models;
using TodoApplication.Application.Dto;
using TodoApplication.Application.Interfaces;

namespace todo.api.Controllers
{
    public class TasksController : AppBaseController
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService )
        {
            _taskService = taskService;
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
            var user = (User)HttpContext.Items["currentUser"];

            if (user == null)
            {
                return NotFound($"User not found");
            }
            var task = await _taskService.CreateTaskAsync(createTaskModel.Description, user);

            return Created(@$"api/tasks/{task.Id}", task);

        }


        /// <summary>
        /// Return All task of logged in user
        /// </summary>
        /// <returns>List of task of logged in User </returns>
        /// <response code="200">List of task of logged in User</response>
        /// <response code="404">If the user not found</response>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTasks()
        {
            var user = (User)HttpContext.Items["currentUser"];

            if (user == null)
            {
                return NotFound($"User not found");
            }
            else
            {
                var tasks = _taskService.GetUserTasks(user.Id);

                return Ok(tasks);
            }

        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="updateTaskModel"> task model with status and description</param>
        /// <param name="id">task id to update</param>
        /// <returns></returns>
        /// <response code="200">If Task Successfully Updated</response>
        /// <response code="404">If the task not found</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskModel updateTaskModel, [FromRoute] Guid id)
        {
            await _taskService.UpdateTaskAsync(id, updateTaskModel);

            return Ok("Task Updated Successfully");
        }



        /// <summary>
        /// Return All tasks of logged in user filetred by status code
        /// </summary>
        /// <param name="status">Status of tasks</param>
        /// <returns>List of tasks</returns>
        /// <response code="200">List of task of logged in User</response>
        /// <response code="404">If the user not found</response>
        [HttpGet("status/{status}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTasksByStatus([FromRoute] Status status)
        {
            var user = (User)HttpContext.Items["currentUser"];

            if (user == null)
            {
                throw new Exception($"User not found");
                //return NotFound($"User not found with id {_userId}");
            }

            var tasks = _taskService.GetUserTasksByStatus(user.Id, status);

            return Ok(tasks);
        }

        /// <summary>
        /// delete task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id)
        {
            await _taskService.DeleteTaskAsync(id);

            return Ok("Task Deleted Successfully");
        }
    }
}
