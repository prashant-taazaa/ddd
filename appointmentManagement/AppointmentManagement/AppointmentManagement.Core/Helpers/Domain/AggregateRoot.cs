using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.Helpers.Domain
{
    public class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; }
    }
}
