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

        public DashboardViewModel()
        {
            // Retrieve the name saved during AuthService.LoginAsync
            // Default to "Guest" if not found
            UserName = Preferences.Default.Get("user_name", "Guest");
        }
    }
}
