using System.Collections.ObjectModel;
using todo.domain.Enums;

namespace todo.domain.Models
{
    public class Task : Aggregate
    {
        private string _description;
        private User _createdBy;
       
        public string Description { get { return _description; } set { _description = value; } }
        public User CreatedBy { get { return _createdBy; } set { _createdBy = value; } }
        public virtual Status Status { get; set; }
        public virtual Collection<Tag> Tags { get; set; } = new Collection<Tag>();

        private Task() { }
        public Task(string description, User user)
        {
            Description = description;
            CreatedBy = user;
            Status = Status.Pending;
        }

        public Task(string description)
        {
            Description = description;
            Status = Status.Pending;
        }
    }
}
