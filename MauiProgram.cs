using CommunityToolkit.Maui;
//using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using RSVPMobile.ViewModels;
using RSVPMobile.Views;

namespace RSVPMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SignupViewModel>();
            builder.Services.AddTransient<EventViewModel>(); 

            // And update your View registration to be Singleton if they are main tabs
            builder.Services.AddSingleton<DashboardView>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<AttendeeView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
