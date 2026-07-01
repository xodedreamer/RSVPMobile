using RSVPMobile.Dtos;
using RSVPMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace RSVPMobile.Services.Events
{
    public class RSVPService : IRSVPService
    {
        private readonly HttpClient _httpClient;

        public RSVPService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IEnumerable<UserEventRsvpResponse>> GetUserEventsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<EventStatusDto>>("users/my-events");

                if (response == null)
                    return Enumerable.Empty<UserEventRsvpResponse>();

                return response.Select(e => new UserEventRsvpResponse(
                    Id: e.EventId,
                    Title: e.Title,
                    Description: "",         // API does not return description
                    Type: "",                // API does not return type
                    EventDate: e.Date,
                    Location: e.Location,
                    ImageUrl: e.ImageUrl,
                    IsConfirmed: e.RsvpStatus == "Confirmed"
                ));
            }
            catch { return Enumerable.Empty<UserEventRsvpResponse>(); }
        }

        public async Task<IEnumerable<UserEventRsvpResponse>> GetEventsAsync()
        {
            try
            {
                // Assuming api/Users/my-events or api/Events handles user-scoped lists
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<UserEventRsvpResponse>>("Events");
                return response ?? Enumerable.Empty<UserEventRsvpResponse>();

            }
            catch { return Enumerable.Empty<UserEventRsvpResponse>(); }
        }


        public async Task<bool> AcceptRsvpAsync(int eventId)
        {
            try
            {
                // Matches: POST http://localhost:5148/api/Users/accept/{eventId}
                var response = await _httpClient.PostAsync($"Users/accept/{eventId}", null);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}