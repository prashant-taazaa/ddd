using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Clients
{
    public class CreditCard
    {
        public virtual Guid Id { get; protected set; }
        public virtual string NameOnCard { get; protected set; }
        public virtual string CardNumber { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        public virtual DateTime Expiry { get; protected set; }
        public virtual Client Client { get; protected set; }

        public static CreditCard Create(Client client, string name, string cardNumber,
            bool active, DateTime expiry)
        {
            if (client == null)
                throw new Exception("Client object cannot be null");

            if (string.IsNullOrEmpty(name))
                throw new Exception("Card name cannot be empty");

            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 16)
                throw new Exception("Card number is incorrect");

            if (DateTime.Now > expiry)
                throw new Exception("Credit card expiry can't be in the past");

            return new CreditCard()
            {
                Id = Guid.NewGuid(),
                Active = active,
                CardNumber = cardNumber,
                Created = DateTime.UtcNow,
                Expiry = expiry,
                NameOnCard = name,
                Client = client
            };
        }


        public override bool Equals(object obj)
        {
            var creditCardToCompare = obj as CreditCard;
            if (creditCardToCompare == null)
                throw new Exception("Can't compare two different objects to each other");

            return this.CardNumber == creditCardToCompare.CardNumber &&
                this.Expiry == creditCardToCompare.Expiry;
        }
    }
}
