using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using todo.building.blocks;

namespace todo.domain.Models
{
    public class Task : Aggregate
    {
        private string _description;
        private User _createdBy;
       
        public string Description { get { return _description; } set { _description = value; } }
        public User CreatedBy { get { return _createdBy; } set { _createdBy = value; } }
        public virtual Status Status { get; set; }
        public virtual Collection<Tag> Tags { get; set; }

        private Task() { }
        public Task(string description, User user)
        {

        }
    }
}
