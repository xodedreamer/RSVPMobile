using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class SignupView : ContentPage
{
	public SignupView(SignupViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    private void OnRoleTapped(object sender, TappedEventArgs e)
    {
        var selectedRole = e.Parameter.ToString();
        var vm = (SignupViewModel)BindingContext;
        vm.SelectedRole = selectedRole; // Update the VM

        VisualStateManager.GoToState(AttendeeCard, selectedRole == "Attendee" ? "Selected" : "Normal");
        VisualStateManager.GoToState(AdminCard, selectedRole == "Admin" ? "Selected" : "Normal");
    }
}