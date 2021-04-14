using System.Collections.ObjectModel;
using todo.domain.Enums;
using System;

namespace todo.domain.Models
{
    public class Task : Aggregate
    {
        public string Description { get; protected set; }
        public User CreatedBy { get; protected set; }
        public Status Status { get; protected set; }
        public virtual Collection<Tag> Tags { get; set; } = new Collection<Tag>();

        public static Task Create(string description, User user)
        {
            if(string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            return new Task()
            {
                Description = description,
                CreatedBy = user,
                Status = Status.Pending

            };
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void AddTag(Tag tag)
        {
            Tags.Add(tag);
        }

    }
}
