using todo.domain.Models;
using todo.infrastructure.shared;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
