using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Appointments
{
    public enum AppointmentStatus
    {
        Pending = 1,
        Started = 2,
        Completed = 3,
        Resheduled = 4
    }
}
