using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Mentors
{
    public class MentorSpecialization
    {
        private List<Mentor> mentors = new List<Mentor>();
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual ReadOnlyCollection<Mentor> Mentors { get { return this.mentors.AsReadOnly(); } }

        public static MentorSpecialization Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static MentorSpecialization Create(Guid id,string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return new MentorSpecialization()
            {
                Id = id,
                Name = name
            };
        }


        public override bool Equals(object obj)
        {
            var mentorSpecializationToCompare = obj as MentorSpecialization;
            if (mentorSpecializationToCompare == null)
                throw new Exception("Can't compare two different objects to each other");

            return this.Id == mentorSpecializationToCompare.Id;
        }

    }
}
