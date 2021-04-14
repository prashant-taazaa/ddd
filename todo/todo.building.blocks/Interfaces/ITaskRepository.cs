using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Enums;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        IList<domain.Models.Task> GetUserTasks(Guid userId);
        IList<domain.Models.Task> GetUserTasksByStatus(Guid userId, Status status);
    }
}
