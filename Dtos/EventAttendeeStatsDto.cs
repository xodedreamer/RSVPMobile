using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public class EventAttendeeStatsDto
    {
        public int Total { get; set; }
        public int Confirmed { get; set; }
        public int Pending { get; set; }
        public int Declined { get; set; }
    }
}
