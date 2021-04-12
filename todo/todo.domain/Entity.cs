using System;
using System.Collections.Generic;
using System.Text;

namespace todo.domain
{
    public abstract class Entity
    {
        public Guid Id { get;  set; }
        public Entity()
        {
            //Id = Guid.NewGuid();
        }
    }
}
