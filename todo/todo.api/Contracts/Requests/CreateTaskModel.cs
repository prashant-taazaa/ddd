using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Contracts.Requests
{
    public class CreateTaskModel
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
