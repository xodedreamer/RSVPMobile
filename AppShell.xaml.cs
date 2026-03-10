using RSVPMobile.Views;

namespace RSVPMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(SignupView), typeof(SignupView));
            Routing.RegisterRoute(nameof(AttendeeView), typeof(AttendeeView));
           // Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
            Routing.RegisterRoute("DashboardView", typeof(DashboardView));
        }
    }
}
