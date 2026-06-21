using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using RSVPMobile.Dtos;
using RSVPMobile.Services.Events;
using RSVPMobile.Models;

namespace RSVPMobile.ViewModels
{
    public partial class AttendeeViewModel : ObservableObject
    {
        private readonly IEventService _eventService;
       [ObservableProperty] private bool _isBusy;
        public ObservableCollection<EventDto> Events { get; } = new();
        public ObservableCollection<Attendee> Attendees { get; } = new();

        private EventDto _selectedEvent;
        public EventDto SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (SetProperty(ref _selectedEvent, value) && value != null)
                {
                    LoadAttendeesCommand.Execute(null);
                }
            }
        }

        private int _total;
        public int Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        private int _confirmed;
        public int Confirmed
        {
            get => _confirmed;
            set => SetProperty(ref _confirmed, value);
        }

        private int _pending;
        public int Pending
        {
            get => _pending;
            set => SetProperty(ref _pending, value);
        }

        public ICommand LoadEventsCommand { get; }
        public ICommand LoadAttendeesCommand { get; }

        public AttendeeViewModel(IEventService eventService)
        {
            _eventService = eventService;

            LoadEventsCommand = new Command(async () => await LoadEventsAsync());
            LoadAttendeesCommand = new Command(async () => await LoadAttendeesAsync());
        }

        public async Task InitializeAsync()
        {
            await LoadEventsAsync();
        }

        private async Task LoadEventsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var events = await _eventService.GetEventsAsync();
                Events.Clear();
                foreach (var e in events)
                    Events.Add(e);

                if (Events.Any() && SelectedEvent == null)
                    SelectedEvent = Events.First();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadAttendeesAsync()
        {
            //if (IsBusy || SelectedEvent == null) return;

            try
            {
                IsBusy = true;

                var result = await _eventService.GetEventAttendeesAsync(SelectedEvent.Id);

                Attendees.Clear();
                foreach (var dto in result.Attendees)
                    Attendees.Add(new Attendee(dto));

                Total = result.Stats.Total;
                Confirmed = result.Stats.Confirmed;
                Pending = result.Stats.Pending;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
