using Android.App;
using Android.Content.PM;
using Android.OS;

namespace RSVPMobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the Status Bar Color to match your #12121B background
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#12121B"));

            // Optional: Make sure the icons (time, battery) are white/light
           // Window.InsetsController?.SetAppearanceLightStatusBars(false);
        }
    }


}
