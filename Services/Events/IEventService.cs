using RSVPMobile.Dtos;
using RSVPMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Events
{
    public interface IEventService
    {
        Task<bool> CreateEventAsync(CreateEventRequest eventData);
        Task<IEnumerable<EventResponse>> GetAllEventsAsync(); // Add this line
    }
}
