using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain.Enums;

namespace todo.api.Contracts.Requests
{
    public class UpdateTaskModel
    {
        public Status Status { get; set; }
        public string Description { get; set; }

    }
}
