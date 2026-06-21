using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
