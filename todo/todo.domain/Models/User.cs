using System;
using System.Collections.ObjectModel;


namespace todo.domain.Models
{
    public class User : Aggregate
    {
        public string Email { get; protected set; }
        public string Name { get; protected set; }

        public virtual Collection<Task> Tasks { get; protected set; } = new Collection<Task>();

        public static User Create(Guid id, string name, string email)
        {
            return new User()
            {
                Id = id,
                Email = email,
                Name = name,
                Tasks = new Collection<Task>()
            };

        }
    }
}

