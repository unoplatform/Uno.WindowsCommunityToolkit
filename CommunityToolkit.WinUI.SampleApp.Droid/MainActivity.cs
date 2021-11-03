using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Content.PM;
using Android.Views;

namespace CommunityToolkit.WinUI.SampleApp.Droid
{
    [Activity(
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
            WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden
        )]
    public class MainActivity : Microsoft.UI.Xaml.ApplicationActivity
    {
    }
}