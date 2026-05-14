using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Dtos;
using RSVPMobile.Services.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSVPMobile.ViewModels
{
    public partial class CreateEventViewModel: ObservableObject
    {
        private readonly IEventService _eventService;

        [ObservableProperty] private string _title;
        [ObservableProperty] private string _eventType;
        [ObservableProperty] private DateTime _eventDate = DateTime.Now;
        [ObservableProperty] private TimeSpan _eventTime = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string _location;
        [ObservableProperty] private string _description;
        [ObservableProperty] private ImageSource _selectedImageSource = "upload_icon.png";
        [ObservableProperty] private bool _isBusy;

        public bool IsNotBusy => !IsBusy;


        public CreateEventViewModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [RelayCommand]
        private async Task PublishEvent()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Location))
            {
                await Shell.Current.DisplayAlertAsync("Error", "Please fill in all required fields", "OK");
                return;
            }

            IsBusy = true;

            var newEvent = new CreateEventRequest(
                 0,
                 Title,
                 Description,
                 GetEventTypeIndex(EventType),
                 EventDate.Date.Add(EventTime),
                 Location,
                 ""
             );

            var success = await _eventService.CreateEventAsync(newEvent);

            IsBusy = false;

            if (success)
            {
                await Shell.Current.DisplayAlertAsync("Success", "Event Published!", "OK");
                await Shell.Current.GoToAsync(".."); // Go back to Dashboard
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to create event. Try again.", "OK");
            }
        }

        private int GetEventTypeIndex(string type) => type switch
        {
            "Wedding" => 0,
            "Party" => 1,
            "Conference" => 2,
            "Workshop" => 3,
            "Concert" => 4,
            _ => 0
        };

        [RelayCommand]
        private async Task PickImage()
        {
            var result = await MediaPicker.Default.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                SelectedImageSource = ImageSource.FromStream(() => stream);
            }
        }
    }
}
