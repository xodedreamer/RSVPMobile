using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
//using static Android.Provider.CalendarContract;

namespace RSVPMobile.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Attendee> _attendees;

        public EventViewModel()
        {
            _attendees = new ObservableCollection<Attendee>
        {
            new Attendee { Name = "Sarah Johnson", Email = "sarah.j@example.com", Status = "Confirmed", Initial = "S" },
            new Attendee { Name = "Michael Chen", Email = "m.chen@techcorp.com", Status = "Confirmed", Initial = "M" },
            new Attendee { Name = "Emily Davis", Email = "emily.d@startup.io", Status = "Pending", Initial = "E" }
        };
        }
    }

    public class Attendee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Initial { get; set; }
        public Color StatusColor => Status == "Confirmed" ? Colors.Green : Colors.Orange;
    }
}
