using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using todo.repositories.Interfaces;
namespace todo.repositories
{
    public class TaskRepository : BaseRepository<domain.Models.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {

        }
    }
}
