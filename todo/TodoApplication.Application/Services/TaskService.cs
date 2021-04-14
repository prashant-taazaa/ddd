using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Enums;
using todo.domain.Exceptions;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;
using TodoApplication.Application.Dto;
using TodoApplication.Application.Interfaces;

namespace TodoApplication.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task<Task> CreateTaskAsync(string description, User user)
        {
            var task = Task.Create(description, user);

            _taskRepository.Add(task);
            await _taskRepository.SaveChangesAsync();

            return task;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            var task = _taskRepository.GetByID(id);

            if (task == null)
                throw new GenericAppException($"Task with id {id} not found");


            _taskRepository.Delete(id);

            await _taskRepository.SaveChangesAsync();
        }

        public IEnumerable<Task> GetUserTasks(Guid id)
        {
            return _taskRepository.GetUserTasks(id);
        }

        public IEnumerable<Task> GetUserTasksByStatus(Guid userId, Status status)
        {
            return _taskRepository.GetUserTasksByStatus(userId, status);
        }
        public async System.Threading.Tasks.Task UpdateTaskAsync(Guid id, UpdateTaskModel updateTaskModel)
        {
            var task = _taskRepository.GetByID(id);

            if (task == null)
                throw new GenericAppException($"Task with id {id} not found");

            if (task.Status != updateTaskModel.Status)
                task.UpdateStatus(updateTaskModel.Status);
            if (!string.IsNullOrEmpty(updateTaskModel.Description) && updateTaskModel.Description != task.Description)
                task.UpdateDescription(updateTaskModel.Description);

            _taskRepository.Update(task);

            await _taskRepository.SaveChangesAsync();
        }
   
    

    }
}
