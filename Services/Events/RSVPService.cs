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