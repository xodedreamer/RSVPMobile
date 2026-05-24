using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Models;
using RSVPMobile.Services.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class RSVPViewModel : ObservableObject
    {
        private readonly IRSVPService _rsvpService;
        private List<UserEventRsvpResponse> _allEvents = new();

        [ObservableProperty] private string _searchText;
        [ObservableProperty] private bool _showConfirmed = false; // False = Pending/All, True = Confirmed
        [ObservableProperty] private bool _isBusy;

        public ObservableCollection<UserEventRsvpResponse> FilteredEvents { get; } = new();

        public RSVPViewModel(IRSVPService rsvpService)
        {
            _rsvpService = rsvpService;
        }

        [RelayCommand]
        public async Task LoadEventsAsync()
        {
            IsBusy = true;
            var events = await _rsvpService.GetUserEventsAsync();
            _allEvents = events.ToList();
            ApplyFilter();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task AcceptRsvp(int eventId)
        {
            var success = await _rsvpService.AcceptRsvpAsync(eventId);
            if (success)
            {
                // Update local state smoothly without full layout re-render
                var target = _allEvents.FirstOrDefault(e => e.Id == eventId);
                if (target != null)
                {
                    _allEvents[_allEvents.IndexOf(target)] = target with { IsConfirmed = true };
                    ApplyFilter();
                }
            }
        }

        [RelayCommand]
        private void FilterPending()
        {
            ShowConfirmed = false;
            ApplyFilter();
        }

        [RelayCommand]
        private void FilterConfirmed()
        {
            ShowConfirmed = true;
            ApplyFilter();
        }

        // Handles live search matching and status toggles instantly
        public void ApplyFilter()
        {
            var query = _allEvents.AsEnumerable();

            // 1. Filter by status selection
            query = query.Where(e => e.IsConfirmed == ShowConfirmed);

            // 2. Filter by Search Query matching text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(e => e.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                         e.Location.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            FilteredEvents.Clear();
            foreach (var ev in query)
            {
                FilteredEvents.Add(ev);
            }
        }

        [RelayCommand]
        private async Task ShowEventDetails(UserEventRsvpResponse selectedEvent)
        {
            if (selectedEvent == null) return;

            // Instantiates our custom visual popover dialog card instance
            var popup = new Views.EventDetailsPopup(selectedEvent);

            // Shows the modal onto the Shell viewport window natively
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

            if (result == null) return;
            else 
            {
                await AcceptRsvpCommand.ExecuteAsync(selectedEvent.Id);
            }
            // Check if the result is not null and is explicitly a boolean true
           
        }
    }
}