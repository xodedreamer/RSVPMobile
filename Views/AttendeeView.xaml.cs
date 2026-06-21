//using AndroidX.Lifecycle;
using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class AttendeeView : ContentPage
{
    private readonly AttendeeViewModel _vm;

    public AttendeeView(AttendeeViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.InitializeAsync();
    }
}