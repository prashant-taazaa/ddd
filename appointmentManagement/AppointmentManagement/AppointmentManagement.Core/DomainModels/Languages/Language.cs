using AppointmentManagement.Core.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Languages
{
    public class Language : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }


        public static Language Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static Language Create (Guid id,string name)
        {
            return new Language()
            {
                Id = id,
                Name = name
            };
        }
    }
}
