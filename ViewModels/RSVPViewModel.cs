using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views; // Required for IPopupResult parsing
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using RSVPMobile.Models;
using RSVPMobile.Services; 
using RSVPMobile.Services.Events;
using RSVPMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Plugin.LocalNotification.AndroidOption;

namespace RSVPMobile.ViewModels;

public partial class RSVPViewModel : ObservableObject
{
    private readonly IRSVPService _rsvpService;
    private List<UserEventRsvpResponse> _allEvents = new();

    [ObservableProperty] private string _searchText;
    [ObservableProperty] private bool _showConfirmed = false; // False = Pending, True = Confirmed
    [ObservableProperty] private bool _isBusy;
    private List<UserEventRsvpResponse> _confirmedEvents;

    public ObservableCollection<UserEventRsvpResponse> FilteredEvents { get; } = new();

    public RSVPViewModel(IRSVPService rsvpService)
    {
        _rsvpService = rsvpService;
    }

    [RelayCommand]
    public async Task LoadEventsAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        try
        {
            // Hits http://localhost:5148/api/events/ via your configured service client layer
            var events = await _rsvpService.GetEventsAsync();
            _allEvents = events.ToList();
            ApplyFilter();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching user assigned events: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AcceptRsvp(int eventId)
    {
        var success = await _rsvpService.AcceptRsvpAsync(eventId);
        if (success)
        {
            // Instantly sync internal data state matching the database modification
            var target = _allEvents.FirstOrDefault(e => e.Id == eventId);
            if (target != null)
            {
                _allEvents[_allEvents.IndexOf(target)] = target with { IsConfirmed = true };
                ApplyFilter();
            }
        }
    }

    [RelayCommand]
    private async Task TentativeRsvp(int eventId)
    {
        var success = await _rsvpService.AcceptRsvpAsync(eventId);
        if (success)
        {
            // Instantly sync internal data state matching the database modification
            var target = _allEvents.FirstOrDefault(e => e.Id == eventId);
            if (target != null)
            {
                _allEvents[_allEvents.IndexOf(target)] = target with { IsConfirmed = true };
                ApplyFilter();
            }
        }
    }

    [RelayCommand]
    private async Task DeclineRsvp(int eventId)
    {
        var success = await _rsvpService.AcceptRsvpAsync(eventId);
        if (success)
        {
            // Instantly sync internal data state matching the database modification
            var target = _allEvents.FirstOrDefault(e => e.Id == eventId);
            if (target != null)
            {
                _allEvents[_allEvents.IndexOf(target)] = target with { IsConfirmed = true };
                ApplyFilter();
            }
        }
    }

    [RelayCommand]
    private async Task FilterPending()
    {
        ShowConfirmed = false;
        await LoadEventsAsync();
    }

    [RelayCommand]
    private async Task FilterConfirmed()
    {
        ShowConfirmed = true;
        await LoadConfirmedEventsAsync();
    }

    public void ApplyFilter()
    {
        IEnumerable<UserEventRsvpResponse> query;

        // 1. Choose the correct source list
        query = ShowConfirmed ? _confirmedEvents : _allEvents;

        // 2. Apply search
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            query = query.Where(e =>
                (e.Title?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (e.Location?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // 3. Update UI
        FilteredEvents.Clear();
        foreach (var ev in query)
            FilteredEvents.Add(ev);
    }

    [RelayCommand]
    private async Task ShowEventDetails(UserEventRsvpResponse ev)
    {
        if (ev == null) return;

        var popup = new EventDetailsPopup(ev);

        popup.ActionSelected += async action =>
        {
            switch (action)
            {
                case "accept":
                    await AcceptRsvp(ev.Id);
                    break;

                case "tentative":
                    await TentativeRsvp(ev.Id);
                    break;

                case "decline":
                    await DeclineRsvp(ev.Id);
                    break;

                case "close":
                default:
                    break;
            }
        };

        await Shell.Current.CurrentPage.ShowPopupAsync(popup);

    }

    [RelayCommand]
    private async Task SetReminder(UserEventRsvpResponse ev)
    {
        if (ev == null) return;

        var popup = new ReminderPopup();

        // Subscribe to the popup callback
        popup.ReminderSelected += async minutes =>
        {
            var notifyTime = ev.EventDate.AddMinutes(-minutes);

            var notification = new NotificationRequest
            {
                NotificationId = ev.Id, // Unique per event
                Title = "Event Reminder",
                Description = $"{ev.Title} starts at {ev.EventDate:MMM dd, HH:mm}",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime
                }
            };

            // Show the notification
            await LocalNotificationCenter.Current.Show(notification);

            // Confirmation alert
            await Shell.Current.DisplayAlert(
                "Reminder Set",
                $"You will be reminded {minutes} minutes before the event.",
                "OK");
        };

        // Show the popup
        await Shell.Current.CurrentPage.ShowPopupAsync(popup);
    }

    [RelayCommand]
    public async Task LoadConfirmedEventsAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var events = await _rsvpService.GetUserEventsAsync(); // Confirmed only
            _confirmedEvents = events.ToList();
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }
}