using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class LoginView : ContentPage
{
	public LoginView( LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm; 
    }

    private async void OnSignUpTapped(object sender, TappedEventArgs e)
    {
        // "SignupView" must match the route name registered in AppShell or MauiProgram
        await Shell.Current.GoToAsync(nameof(SignupView));
    }
}