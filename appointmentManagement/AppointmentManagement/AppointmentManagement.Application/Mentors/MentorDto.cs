using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManagement.Application.Mentors
{
    public class MentorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid LanguageId { get; set; }
        public List<Guid> Specializations { get; set; }
    }
}
