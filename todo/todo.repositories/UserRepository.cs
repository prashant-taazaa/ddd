using todo.domain.Models;
using todo.infrastructure.shared;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.persistence
{
    public class UserRepository : BaseRepository<User,ApplicationDbContext>, IUserRepository
    {
        public UserRepository(IDbContext<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }
    }
}
