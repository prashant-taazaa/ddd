﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TasksController : AppBaseController
    {
        private readonly IUnitOfWork<ApplicationDbContext> _uf;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        public TasksController(            ITaskRepository taskRepository, IUserRepository userRepository)
        {
            //_uf = new UnitOfWork<ApplicationDbContext>(applicationDbContext);
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskModel createTaskModel)
        {
            var context = HttpContext.User.Identity;

            var user = _userRepository.GetByID(createTaskModel.UserId);

            var task = user.CreateTask(createTaskModel.Description);

            _taskRepository.Add(task);

            await _taskRepository.SaveChangesAsync();

            return Created(@$"api/tasks/{task.Id}", task);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetTasksAsync([FromQuery] Guid userId)
        {

            var context = HttpContext.User.Identity;

            var user = _userRepository.GetByID(userId);
            

            return Ok(user.Tasks);
        }
    }
}
