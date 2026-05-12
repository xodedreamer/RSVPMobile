using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class DashboardView : ContentPage
{
	public DashboardView(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

}