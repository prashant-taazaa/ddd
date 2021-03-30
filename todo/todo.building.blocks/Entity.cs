using System;
using System.Collections.Generic;
using System.Text;

namespace todo.building.blocks
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
