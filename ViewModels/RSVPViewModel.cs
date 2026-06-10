using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views; // Required for IPopupResult parsing
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Models;
using RSVPMobile.Services; 
using RSVPMobile.Services.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RSVPMobile.ViewModels;

public partial class RSVPViewModel : ObservableObject
{
    private readonly IRSVPService _rsvpService;
    private List<UserEventRsvpResponse> _allEvents = new();

    [ObservableProperty] private string _searchText;
    [ObservableProperty] private bool _showConfirmed = false; // False = Pending, True = Confirmed
    [ObservableProperty] private bool _isBusy;

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
            var events = await _rsvpService.GetUserEventsAsync();
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

    public void ApplyFilter()
    {
        var query = _allEvents.AsEnumerable();

        // 1. Filter by Status matching the targeted stream tab mode
        query = query.Where(e => e.IsConfirmed == ShowConfirmed);

        // 2. Filter by Search Query string criteria matching
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

        await Shell.Current.GoToAsync("///eventdetails", new Dictionary<string, object>
        {
            { "EventDetails", selectedEvent }
        });

    }
}