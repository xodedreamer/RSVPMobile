//using AndroidX.Lifecycle;
using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class AttendeeView : ContentPage
{

    private readonly EventViewModel _viewModel;
    private const uint AnimationDuration = 800u;
    public AttendeeView()
	{
		InitializeComponent();

        _viewModel = new EventViewModel();
        this.BindingContext = _viewModel;
    }
}