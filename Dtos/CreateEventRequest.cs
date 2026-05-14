using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public record CreateEventRequest(
    int id,
    string title,
    string description,
    int type,
    DateTime eventDate, 
    string location,
    string imageUrl
);
}
