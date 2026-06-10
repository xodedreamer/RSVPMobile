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
        private readonly IUserSessionService _sessionService;
        [ObservableProperty] private string _email = string.Empty;
        [ObservableProperty] private string _userName = string.Empty;
        [ObservableProperty] private string _phoneNumber= string.Empty;
        [ObservableProperty] private string _memberSince = string.Empty;

        public ProfileViewModel(IAuthService authService, IUserSessionService session)
        {
            _authService = authService;
            _sessionService = session;

            UserName = _sessionService.UserName;
            Email = _sessionService.Email;
            PhoneNumber = _sessionService.PhoneNumber;
            MemberSince = $"Member since {_sessionService.MemberSince:MMMM yyyy}";
        }

        [RelayCommand]
        private async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlertAsync("Logout", "Are you sure you want to sign out?", "Yes", "No");

            if (answer)
            {
                await _authService.LogoutAsync();
                SecureStorage.RemoveAll();
                // AuthService.LogoutAsync should call:
                // await Shell.Current.GoToAsync("//LoginView");
            }
        }
    }
}
