using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class CreateEventView : ContentPage
{
	public CreateEventView(CreateEventViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}