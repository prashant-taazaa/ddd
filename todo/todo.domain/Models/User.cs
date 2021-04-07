using System;
using System.Collections.ObjectModel;


namespace todo.domain.Models
{
    public class User : Aggregate
    {
        private string _email;

        private string _name;
             
        public string Email { get { return _email; } set { _email = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public virtual Collection<Task> Tasks { get; protected set; } = new Collection<Task>();

        private User() { }

        public User(Guid id, string name, string email)
        {
            Id = id;
            Email = email;
            Name = name;
            Tasks = new Collection<Task>();
        }

        public Task CreateTask(string description)
        {
            var task = new Task(description, this);
            Tasks.Add(task);

            return task;
        }
    }
}
