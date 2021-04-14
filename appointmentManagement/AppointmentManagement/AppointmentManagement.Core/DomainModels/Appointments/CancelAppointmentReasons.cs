using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Appointments
{
    public enum CancelAppointmentReasons
    {
        ClientNotAvailable = 1,
        MentorNotAvailable = 2,
        Other = 3
    }
}
