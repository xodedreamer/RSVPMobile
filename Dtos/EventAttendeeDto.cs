using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public class EventAttendeeDto
    {
        public int id { get; set; }   // FIXED
        public string fullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateTime rsvpDate { get; set; }
    }
}
