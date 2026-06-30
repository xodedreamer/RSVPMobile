using CommunityToolkit.Maui.Views;
using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class ReminderPopup : Popup
{
    public Command<string> SelectReminderCommand { get; set; }
    public Action<int>? ReminderSelected;
    public ReminderPopup()
    {
        InitializeComponent();
    }

    private async void TenMinutes_Clicked(object sender, EventArgs e)
    {
        ReminderSelected?.Invoke(10);
        await CloseAsync();
    }

    private async void ThirtyMinutes_Clicked(object sender, EventArgs e)
    {
        ReminderSelected?.Invoke(30);
        await CloseAsync();
    }

    private async void Hour_Clicked(object sender, EventArgs e)
    {
        ReminderSelected?.Invoke(60);
        await CloseAsync();
    }
}