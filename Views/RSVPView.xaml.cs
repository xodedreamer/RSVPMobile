using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class RSVPView : ContentPage
{
	public RSVPView(RSVPViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RSVPViewModel vm)
        {
            await vm.LoadEventsAsync();
        }
    }
}