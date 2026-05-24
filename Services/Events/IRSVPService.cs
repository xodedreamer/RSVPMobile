using RSVPMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Events
{
    public interface IRSVPService
    {
        Task<IEnumerable<UserEventRsvpResponse>> GetUserEventsAsync();
        Task<bool> AcceptRsvpAsync(int eventId);
    }
}
