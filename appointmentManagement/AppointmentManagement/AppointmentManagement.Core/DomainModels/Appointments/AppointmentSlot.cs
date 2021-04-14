using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Appointments
{
   public class AppointmentSlot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
