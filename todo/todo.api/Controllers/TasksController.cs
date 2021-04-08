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
using todo.infrastructure.shared.Interfaces;

namespace todo.api.Controllers
{
    public class TasksController : AppBaseController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public TasksController(ITaskRepository taskRepository, 
            IUserRepository userRepository,
            IMapper mapper):base(userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
            if (_user == null)
            {
                return NotFound($"User not found with id {_userId}");
            }

            var task = _user.CreateTask(createTaskModel.Description);

            _taskRepository.Add(task);

            await _taskRepository.SaveChangesAsync();

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
         

            if (_user == null)
            {
                return NotFound($"User not found with id {_userId}");
            }
            else
            {
                var tasks = _taskRepository.GetUserTasks(_user);

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
            var task = _taskRepository.GetByID(id);

            if (task == null)
                return NotFound($"Task with id {id} not found");

            task.Status = updateTaskModel.Status;
            task.Description = updateTaskModel.Description ?? task.Description;

            _taskRepository.Update(task);

            await _taskRepository.SaveChangesAsync();

            return Ok("Task Updated Successfully");
        }



        /// <summary>
        /// Return All tasks of logged in user filetred by status code
        /// </summary>
        /// <param name="status">Status of tasks</param>
        /// <returns>List of tasks</returns>
        /// <response code="200">List of task of logged in User</response>
        /// <response code="404">If the user not found</response>
        [HttpGet("get-by-status/{status}")]
        //[Authorize(Policy = "Admin")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTasksByStatus([FromRoute] Status status)
        {
          

            if (_user == null)
            {
                throw new Exception($"User not found with id {_userId}");
                //return NotFound($"User not found with id {_userId}");
            }

            var tasks = _taskRepository.GetUserTasksByStatus(_user, status);

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
            var task = _taskRepository.GetByID(id);

            if (task == null)
                return NotFound($"Task with id {id} not found");

            _taskRepository.Delete(id);

            await _taskRepository.SaveChangesAsync();

            return Ok("Task Deleted Successfully");
        }
    }
}
