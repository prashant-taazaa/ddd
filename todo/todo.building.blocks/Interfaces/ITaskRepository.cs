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
        IList<domain.Models.Task> GetUserTasks(User user);
        IList<domain.Models.Task> GetUserTasksByStatus(User user, Status status);
    }
}
