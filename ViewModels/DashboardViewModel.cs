using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Models;
using RSVPMobile.Services.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IEventService _eventService;

        [ObservableProperty] private string _userName;
        [ObservableProperty] private bool _isAdmin;
        [ObservableProperty] private bool _isFabExpanded;
        [ObservableProperty] private bool _isRefreshing;
        [ObservableProperty] private int _totalEventsCount;
        [ObservableProperty] private int _confirmedCount;
        [ObservableProperty] private int _tentativeCount;
        [ObservableProperty] private int _declinedCount;
        [ObservableProperty] private int _pendingCount;
        // ObservableCollection updates the UI elements instantly when items load
        public ObservableCollection<EventResponse> Events { get; } = new();

        public DashboardViewModel(IEventService eventService)
        {
            _eventService = eventService;

            UserName = Preferences.Default.Get("user_name", "Guest");
            var role = Preferences.Default.Get("user_role", "Attendee");
            IsAdmin = role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        [RelayCommand]
        public async Task LoadDashboardDataAsync()
        {
            IsRefreshing = true;
            try
            {
                var serverEvents = await _eventService.GetAllEventsAsync();

                Events.Clear();
                if (serverEvents != null)
                {
                    foreach (var ev in serverEvents)
                    {
                        Events.Add(ev);
                    }
                    TotalEventsCount = Events.Count;
                }

                // 2. Load RSVP stats
                var stats = await _eventService.GetRsvpStatsAsync();

                ConfirmedCount = stats.Confirmed;
                TentativeCount = stats.Tentative;
                DeclinedCount = stats.Declined;
                PendingCount = stats.Pending;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Dashboard] Failed to pull feed: {ex.Message}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
