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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork<ApplicationDbContext> _uf;
        private readonly ApplicationDbContext _dbContext;

        public TasksController(IDbContext<ApplicationDbContext> applicationDbContext)
        {
            _uf = new UnitOfWork<ApplicationDbContext>(applicationDbContext);
            _dbContext = applicationDbContext as ApplicationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskModel createTaskModel)
        {
            var context = HttpContext.User.Identity;

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == createTaskModel.UserId);
            
            var task = user.CreateTask(createTaskModel.Description);

            _dbContext.Tasks.Add(task);

            await _uf.CompleteAsync();

            return Created(@$"api/tasks/{task.Id}", task);
        }
    }
}
