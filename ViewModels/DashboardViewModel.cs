using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _isFabExpanded;

        [ObservableProperty]
        private bool _isAdmin;

        public DashboardViewModel()
        {
            // Retrieve the name saved during AuthService.LoginAsync
            // Default to "Guest" if not found
            UserName = Preferences.Default.Get("user_name", "Guest");
            // Check if the stored role is "Admin"
            var role = Preferences.Default.Get("user_role", "Attendee");
            IsAdmin = role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }
    }
}
