using System.Collections.ObjectModel;
using todo.building.blocks;

namespace todo.domain.Models
{
    public class Status : Aggregate
    {
        public string Text { get; set; }
        public virtual Collection<Task> Tasks { get; set; }
    }
}