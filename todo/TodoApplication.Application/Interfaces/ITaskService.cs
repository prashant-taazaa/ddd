using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Enums;
using todo.domain.Models;
using TodoApplication.Application.Dto;

namespace TodoApplication.Application.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<Task> CreateTaskAsync(string description, User user);
        IEnumerable<Task> GetUserTasks(Guid id);
        System.Threading.Tasks.Task UpdateTaskAsync(Guid id, UpdateTaskModel updateTaskModel);
        System.Threading.Tasks.Task DeleteTaskAsync(Guid id);
        IEnumerable<Task> GetUserTasksByStatus(Guid userId, Status status);
    }
}
