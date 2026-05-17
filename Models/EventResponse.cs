using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Models
{
    public record EventResponse(
     int Id,
     string Title,
     string Description,
     string Type,
     DateTime EventDate,
     string Location,
     string ImageUrl
 );
}
