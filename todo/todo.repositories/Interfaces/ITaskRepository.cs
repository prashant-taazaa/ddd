using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
    }
}
