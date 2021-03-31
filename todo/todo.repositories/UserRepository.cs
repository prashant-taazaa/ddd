using todo.domain.Models;
using todo.infrastructure.persistence.Interfaces;
using todo.infrastructure.shared;
using todo.infrastructure.shared.Data;

namespace todo.infrastructure.persistence
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
