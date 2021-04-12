using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using todo.domain.Enums;
using todo.domain.Models;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class TaskFilesRepository : ITaskRepository
    {
        private readonly ApplicationFilesDbContext _dbContext;
        public TaskFilesRepository(ApplicationFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(domain.Models.Task entity)
        {
            _dbContext.Tasks.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = _dbContext.Tasks.GetById(id);
            _dbContext.Tasks.Remove(entity);
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<domain.Models.Task> GetAll()
        {
           return _dbContext.Tasks.Get();
        }

        public domain.Models.Task GetByID(Guid id)
        {
            return _dbContext.Tasks.GetById(id);
        }

        public IList<domain.Models.Task> GetUserTasks(User user)
        {
            throw new NotImplementedException();
        }

        public IList<domain.Models.Task> GetUserTasksByStatus(User user, Status status)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(domain.Models.Task entity)
        {
            throw new NotImplementedException();
        }
    }
}
