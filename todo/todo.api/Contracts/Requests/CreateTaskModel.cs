using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Contracts.Requests
{
    public class CreateTaskModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
