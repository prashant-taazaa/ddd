using AppointmentManagement.Core.DomainModels.Languages;
using AppointmentManagement.Core.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Clients
{
    public class Client : IAggregateRoot
    {
        private List<CreditCard> creditCards = new List<CreditCard>();

        public virtual Guid Id { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual Guid LanguageId { get; protected set; }

        public static Client Create(string firstname, string lastname, string email, Language language)
        {
            return Create(Guid.NewGuid(), firstname, lastname, email, language);
        }

        public static Client Create(Guid id, string firstname, string lastname, string email, Language language)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (language == null)
                throw new ArgumentNullException("language");

            return new Client()
            {
                Id = id,
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                LanguageId = language.Id
            };
        }

        public virtual ReadOnlyCollection<CreditCard> CreditCards { get { return this.creditCards.AsReadOnly(); } }

        public virtual void ChangeEmail(string email)
        {
            if (this.Email != email)
            {
                this.Email = email;
            }
        }

        public virtual void AddCard(CreditCard creditCard)
        {
            this.creditCards.Add(creditCard);
        }
    }
}
