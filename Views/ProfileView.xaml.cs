using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}