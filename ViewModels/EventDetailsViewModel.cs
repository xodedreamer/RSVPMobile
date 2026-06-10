using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class EventDetailsViewModel : ObservableObject
    {
        public UserEventRsvpResponse Event { get; }

        private readonly INavigation _navigation;

        public EventDetailsViewModel(UserEventRsvpResponse eventDetails, INavigation navigation)
        {
            Event = eventDetails;
            _navigation = navigation;
        }

        [RelayCommand]
        private async Task Accept()
        {
            // Call your API or command
            await Shell.Current.DisplayAlert("RSVP", "You accepted the event.", "OK");
            await _navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Tentative()
        {
            await Shell.Current.DisplayAlert("RSVP", "You marked as tentative.", "OK");
            await _navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Decline()
        {
            await Shell.Current.DisplayAlert("RSVP", "You declined the event.", "OK");
            await _navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Close()
        {
            await _navigation.PopAsync();
        }
    }

}
