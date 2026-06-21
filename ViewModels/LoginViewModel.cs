using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using RSVPMobile.Dtos;
using RSVPMobile.Services.Authentication;
using RSVPMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private bool _isBusy;
 
        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        private readonly IAuthService _authService;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            Email = "lindo@gmail.com";
            Password = "Test@01";

        }

        [RelayCommand]
        public async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlertAsync("Error", "Please enter credentials", "OK");
                return;
            }

            IsBusy = true;
            var success = await _authService.LoginAsync(new LoginRequest(Email, Password));
            IsBusy = false;

            if (success)
            {
                await Shell.Current.GoToAsync("//DashboardView");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Login Failed", "Invalid email or password", "OK");
            }
        }
    }
}
