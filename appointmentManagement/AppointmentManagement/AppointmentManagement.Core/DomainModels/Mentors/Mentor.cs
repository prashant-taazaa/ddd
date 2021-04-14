using AppointmentManagement.Core.DomainModels.Languages;
using AppointmentManagement.Core.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Mentors
{
    public class Mentor : IAggregateRoot
    {
        private List<MentorSpecialization> specializations = new List<MentorSpecialization>();

        public Guid Id { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual Guid LanguageId { get; protected set; }

        public virtual ReadOnlyCollection<MentorSpecialization> Mentors { get { return this.specializations
                    .AsReadOnly(); } }

        public static Mentor Create(string firstname, string lastname, string email,
            Language language, List<MentorSpecialization> specializations)
        {
            return Create(Guid.NewGuid(), firstname, lastname, email, language,specializations);
        }

        public static Mentor Create(Guid id, string firstname, string lastname,
            string email, Language language, List<MentorSpecialization> specializations)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (language == null)
                throw new ArgumentNullException("language");

            if(specializations ==null || specializations.Count == 0)
                throw new ArgumentNullException("at least one specialization needed");

            return new Mentor()
            {
                Id = id,
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                LanguageId = language.Id,
                specializations = specializations
            };
        }


        public virtual void ChangeEmail(string email)
        {
            if (this.Email != email)
            {
                this.Email = email;
            }
        }

        public virtual void AddSpecialization(MentorSpecialization specialization)
        {
            this.specializations.Add(specialization);
        }

    }
}
