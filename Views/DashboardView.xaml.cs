using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class DashboardView : ContentPage
{
	public DashboardView(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    private void OnFabHoverEntered(object sender, PointerEventArgs e)
    {
        var vm = (DashboardViewModel)BindingContext;
        vm.IsFabExpanded = true;

        // Optional: Animate the width for a smoother "expand" feel
        FabBorder.Animate("Expand", x => FabBorder.WidthRequest = x, 60, 220, length: 250, easing: Easing.CubicOut);
    }

    private void OnFabHoverExited(object sender, PointerEventArgs e)
    {
        var vm = (DashboardViewModel)BindingContext;
        vm.IsFabExpanded = false;

        // Animate back to a circle
        FabBorder.Animate("Collapse", x => FabBorder.WidthRequest = x, 220, 60, length: 250, easing: Easing.CubicIn);
    }

    private async void OnCreateEventClicked(object sender, EventArgs e)
    {
        try
        {
           // await DisplayAlertAsync("Navigation ", "]Eevent creation screen.", "OK");
            // Use Shell navigation to push the CreateEventView onto the stack
            // nameof() ensures that if you rename the class, the navigation won't break
            await Shell.Current.GoToAsync(nameof(CreateEventView));
        }
        catch (Exception ex)
        {
            // For a Senior Developer role, it's good practice to log or display errors
            System.Diagnostics.Debug.WriteLine($"Navigation failed: {ex.Message}");
            await DisplayAlertAsync("Navigation Error", "Could not open the event creation screen.", "OK");
        }
    }

}