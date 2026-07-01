using CommunityToolkit.Maui.Views;
using RSVPMobile.Models;

namespace RSVPMobile.Views;

// Change "public partial class EventDetailsPopup : ContentPage" to:
public partial class EventDetailsPopup : Popup
{
    public Action<string>? ActionSelected;

    public EventDetailsPopup(UserEventRsvpResponse ev)
    {
        InitializeComponent();
        BindingContext = ev;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        ActionSelected?.Invoke("accept");
        await CloseAsync();
    }

    private async void Tentative_Clicked(object sender, EventArgs e)
    {
        ActionSelected?.Invoke("tentative");
        await CloseAsync();
    }

    private async void Decline_Clicked(object sender, EventArgs e)
    {
        ActionSelected?.Invoke("decline");
        await CloseAsync();
    }

    private async void Close_Clicked(object sender, EventArgs e)
    {
        ActionSelected?.Invoke("close");
        await CloseAsync();
    }
}