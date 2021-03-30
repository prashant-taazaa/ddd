using System;
using System.Collections.Generic;
using System.Text;
using todo.building.blocks;

namespace todo.domain.Models
{
    public class User : Aggregate
    {
        private string _email;

        public string Email { get { return _email; } set { _email = value; } }
    }
}
