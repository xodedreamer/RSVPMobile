using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public class RsvpStatsDto
    {
        public int Confirmed { get; set; }
        public int Tentative { get; set; }
        public int Declined { get; set; }
        public int Pending { get; set; }
    }
}
