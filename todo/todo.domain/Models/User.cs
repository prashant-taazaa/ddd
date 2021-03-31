using System.Collections.ObjectModel;


namespace todo.domain.Models
{
    public class User : Aggregate
    {
        private string _email;

        public string Email { get { return _email; } set { _email = value; } }
        public virtual Collection<Task> Tasks { get; protected set; }

        private User() { }
        public Task CreateTask(string description)
        {
            var task = new Task(description, this);
            Tasks.Add(task);

            return task;
        }
    }
}
