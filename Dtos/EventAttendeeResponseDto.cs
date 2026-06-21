using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public class EventAttendeeResponseDto
    {
        public List<EventAttendeeDto> Attendees { get; set; } = new();
        public EventAttendeeStatsDto Stats { get; set; } = new();
    }
}
