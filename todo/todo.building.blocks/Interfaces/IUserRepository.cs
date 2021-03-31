using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
