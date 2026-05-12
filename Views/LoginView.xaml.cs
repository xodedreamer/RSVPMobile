using RSVPMobile.ViewModels;

namespace RSVPMobile.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
         InitializeComponent();
         BindingContext = viewModel;

    }

    private async void OnSignUpTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupView));
    }
}