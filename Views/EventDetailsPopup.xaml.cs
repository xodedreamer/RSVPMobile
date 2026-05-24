using CommunityToolkit.Maui.Views;
using RSVPMobile.Models;

namespace RSVPMobile.Views;

// Change "public partial class EventDetailsPopup : ContentPage" to:
public partial class EventDetailsPopup : Popup
{
    public EventDetailsPopup(UserEventRsvpResponse eventDetails)
    {
        InitializeComponent();
        BindingContext = eventDetails;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void OnAcceptClicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}