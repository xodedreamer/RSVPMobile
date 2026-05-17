using RSVPMobile.Dtos;
using RSVPMobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace RSVPMobile.Services.Events
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateEventAsync(CreateEventRequest eventData)
        {
            try
            {
                // Endpoint: http://localhost:5148/api/Events/create-event
                var response = await _httpClient.PostAsJsonAsync("Events/create-event", eventData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating event: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<EventResponse>> GetAllEventsAsync()
        {
            try
            {
               
                // The AuthHeaderHandler handles token injection cleanly before this line executes
                var events = await _httpClient.GetFromJsonAsync<IEnumerable<EventResponse>>("events");
                System.Diagnostics.Debug.WriteLine($"[EventService] SENDING GETEVENTS: {events}");
                return events ?? Enumerable.Empty<EventResponse>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EventService] Error fetching events feed: {ex.Message}");

                // Return an empty list instead of throwing to prevent the mobile app from crashing
                return Enumerable.Empty<EventResponse>();
            }
        }
    }
}
