using System;
using System.Collections.Generic;
using System.Linq;
using todo.domain.Enums;
using todo.domain.Models;
using todo.infrastructure.shared;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class TaskRepository : BaseRepository<domain.Models.Task,ApplicationDbContext>, ITaskRepository
    {
        public TaskRepository(IDbContext<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }

        public IList<Task> GetUserTasks(User user)
        {
          return  _dbSet.Where(x => x.CreatedBy == user).ToList();
        }

        public IList<Task> GetUserTasksByStatus(User user, Status status)
        {
            var tasks = from task in _dbSet
                        where task.Status == status
                        && task.CreatedBy == user
                        select task;
           
            return tasks.ToList();
                      
        }
    }
}
