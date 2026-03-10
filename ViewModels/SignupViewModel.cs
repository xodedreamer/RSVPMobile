using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RSVPMobile.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedRole = "Attendee"; // Default selection

        [RelayCommand]
        private void SelectRole(string role)
        {
            SelectedRole = role;
        }
    }
}
