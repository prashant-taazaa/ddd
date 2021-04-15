using AppointmentManagement.Core.DomainModels.Clients;
using AppointmentManagement.Core.DomainModels.Mentors;
using AppointmentManagement.Core.DomainModels.Payments;
using AppointmentManagement.Core.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppointmentManagement.Core.DomainModels.Appointments
{
    public class Appointment : IAggregateRoot
    {
        private List<AppointmentNote> notes = new List<AppointmentNote>();
        private List<AppointmentRefrence> refrences = new List<AppointmentRefrence>();

        public Guid Id { get; protected set; }
        public virtual AppointmentSlot AppointmentSlot { get; protected set; }
        public virtual Payment Payment { get; protected set; }
        public virtual Client Client { get; protected set; }
        public virtual Mentor Mentor { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual AppointmentStatus Status { get; protected set; }
        public CancelAppointment  CancelAppointment { get; set; }

        public virtual ReadOnlyCollection<AppointmentNote> Notes { get { return this.notes.AsReadOnly(); } }
        public virtual ReadOnlyCollection<AppointmentRefrence> Refrences { get { return this.refrences.AsReadOnly(); } }

        public static Appointment Create(AppointmentSlot appointmentSlot, Payment payment,
            Client client,Mentor mentor)
        {
            return Create(Guid.NewGuid(), appointmentSlot, payment, client, mentor);
        }

        public static Appointment Create(Guid id, AppointmentSlot appointmentSlot, Payment payment,
           Client client, Mentor mentor)
        {

            return new Appointment()
            {
                Id = id,
                AppointmentSlot = appointmentSlot,
                Client = client,
                CreatedAt = DateTime.UtcNow,
                Mentor = mentor,
                Payment = payment,
                Status = AppointmentStatus.Pending
            };
        }

        public void AddNote(AppointmentNote appointmentNote)
        {
            notes.Add(appointmentNote);
        }

        public void AddRefrence(AppointmentRefrence  appointmentRefrence)
        {
            refrences.Add(appointmentRefrence);
        }
    }
}
