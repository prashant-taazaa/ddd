using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.Helpers.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
