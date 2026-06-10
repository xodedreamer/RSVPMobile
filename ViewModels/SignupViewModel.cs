using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSVPMobile.Dtos;
using RSVPMobile.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        [ObservableProperty] private string _fullName;
        [ObservableProperty] private string _email;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _confirmPassword;
        [ObservableProperty] private string _selectedRole = "Attendee"; // Default
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string _phoneNumber;

        private readonly IAuthService _authService;

        public SignupViewModel(IAuthService authService) => _authService = authService;

        [RelayCommand]
        public async Task Signup()
        {
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Passwords do not match", "OK");
                return;
            }

            IsBusy = true;
            var request = new SignupRequest(FullName, Email, Password, SelectedRole, PhoneNumber);
            var success = await _authService.SignupAsync(request);
            IsBusy = false;

            if (success)
            {
                await Shell.Current.DisplayAlertAsync("Success", "Account created! Please login.", "OK");
                await Shell.Current.GoToAsync(".."); // Go back to Login
            }
        }
    }
}
