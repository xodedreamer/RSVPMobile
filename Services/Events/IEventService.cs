using RSVPMobile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Events
{
    public interface IEventService
    {
        Task<bool> CreateEventAsync(CreateEventRequest eventData);
    }
}
