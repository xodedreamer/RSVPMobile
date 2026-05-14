using RSVPMobile.Dtos;
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
    }
}
