using CommunityToolkit.Maui;
//using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using RSVPMobile.Services.Authentication;
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

            //1. Configure HttpClient with your API endpoint
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5148/api/"
                : "http://localhost:5148/api/";

            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddTransient<SignupView>();
            builder.Services.AddTransient<SignupViewModel>();

            builder.Services.AddTransient<CreateEventView>();
            builder.Services.AddTransient<EventViewModel>();

            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<ProfileViewModel>();

            builder.Services.AddTransient<RSVPView>();
            builder.Services.AddTransient<QRPassView>();


            // And update your View registration to be Singleton if they are main tabs
            builder.Services.AddSingleton<DashboardView>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<AttendeeView>();

            // 2. Register ViewModels and Pages
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
