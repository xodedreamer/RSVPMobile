using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task SignIn()
        {
            // Add your login validation logic here

            // Navigate to the Dashboard
            // Use "//" to reset the navigation stack so the user can't "back" into the login screen
          //  await Shell.Current.GoToAsync("//DashboardView");
            await Shell.Current.GoToAsync(nameof(DashboardView));
        }
    }


}
