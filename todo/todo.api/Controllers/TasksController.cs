using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Requests;
using todo.api.Extensions;
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
            IMapper mapper)
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

            var sid = HttpContext.GetUserSid();

            var user = _userRepository.GetByID(sid);

            if (user == null)
            {
                return NotFound($"User not found with id {sid}");
            }

            var task = user.CreateTask(createTaskModel.Description);

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
        public async Task<IActionResult> GetTasksAsync()
        {
            var sid = HttpContext.GetUserSid();

            var user = _userRepository.GetByID(sid);

            if (user == null)
            {
                return NotFound($"User not found with id {sid}");
            }
            else
            {
                var tasks = _taskRepository.GetAll().Where(x => x.CreatedBy == user).ToList();

                return Ok(tasks);
            }

        }
    }
}
