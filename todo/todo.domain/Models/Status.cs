using System.Collections.ObjectModel;

namespace todo.domain.Models
{
    public class Status : Aggregate
    {
        public string Text { get; set; }
        public virtual Collection<Task> Tasks { get; set; }
    }
}