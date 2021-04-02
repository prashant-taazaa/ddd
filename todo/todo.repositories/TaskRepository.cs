using System;
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
    }
}
