using System;
using todo.infrastructure.persistence.Interfaces;
using todo.infrastructure.shared;
using todo.infrastructure.shared.Data;

namespace todo.infrastructure.persistence
{
    public class TaskRepository : BaseRepository<domain.Models.Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
