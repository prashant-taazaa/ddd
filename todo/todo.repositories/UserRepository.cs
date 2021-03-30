using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Models;
using todo.repositories.Interfaces;

namespace todo.repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TodoDbContext dbContext) : base(dbContext)
        {

        }
    }
}
