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

        public IList<Task> GetUserTasks(Guid userId)
        {
          return  _dbSet.Where(x => x.CreatedBy.Id == userId).ToList();
        }

        public IList<Task> GetUserTasksByStatus(Guid userId, Status status)
        {
            var tasks = from task in _dbSet
                        where task.Status == status
                        && task.CreatedBy.Id == userId
                        select task;
           
            return tasks.ToList();
                      
        }
    }
}
