using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.domain.Enums;

namespace TodoApplication.Application.Dto
{
    public class UpdateTaskModel
    {
        public Status Status { get; set; }
        public string Description { get; set; }

    }
}
