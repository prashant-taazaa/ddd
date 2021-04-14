using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Appointments
{
    public class CancelAppointment
    {
        public virtual Appointment Appointment { get; protected set; }
        public virtual CancelAppointmentReasons Reason { get; set; }
        public virtual DateTime Created { get; protected set; }
        public virtual string Note { get; protected set; }
    }
}
