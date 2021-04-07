using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain.Enums;

namespace todo.api.Contracts.Response
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
