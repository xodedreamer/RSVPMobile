using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        public ProfileViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlertAsync("Logout", "Are you sure you want to sign out?", "Yes", "No");

            if (answer)
            {
                await _authService.LogoutAsync();
                // AuthService.LogoutAsync should call:
                // await Shell.Current.GoToAsync("//LoginView");
            }
        }
    }
}
