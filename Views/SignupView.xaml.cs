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
        string selectedRole = e.Parameter.ToString();

        if (selectedRole == "Attendee")
        {
            VisualStateManager.GoToState(AttendeeCard, "Selected");
            VisualStateManager.GoToState(AdminCard, "Normal");

            // Optional: Update a Label or variable to store the selection
            //SelectedRoleLabel.Text = "Attendee selected"; 
            
        }
        else
        {
            VisualStateManager.GoToState(AdminCard, "Selected");
            VisualStateManager.GoToState(AttendeeCard, "Normal");

            //SelectedRoleLabel.Text = "Admin selected";
        }
    }
}