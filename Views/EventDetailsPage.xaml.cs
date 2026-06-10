using RSVPMobile.Models;
using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class EventDetailsPage : ContentPage
{
    public UserEventRsvpResponse EventDetails
    {
        set
        {
            BindingContext = new EventDetailsViewModel(value, Navigation);
        }
    }

    public EventDetailsPage()
    {
        InitializeComponent();
    }
}