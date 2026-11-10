using Android.App;
using Android.Content.PM;

using Android.OS;
using Android.Views;

namespace MoneyControlApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            var contenido = FindViewById(Android.Resource.Id.Content);
            if (contenido is not null)
            {
                contenido.SetBackgroundColor(Android.Graphics.Color.ParseColor("#103B31"));
                contenido.SetOnApplyWindowInsetsListener(new MargenBarrasListener());
            }
        }

        private sealed class MargenBarrasListener : Java.Lang.Object, Android.Views.View.IOnApplyWindowInsetsListener
        {
            public WindowInsets? OnApplyWindowInsets(Android.Views.View? v, WindowInsets? insets)
            {
                if (v is null || insets is null) return insets;

                if (OperatingSystem.IsAndroidVersionAtLeast(30))
                {
                    var barras = insets.GetInsets(WindowInsets.Type.SystemBars());
                    v.SetPadding(barras.Left, barras.Top, barras.Right, barras.Bottom);
                    return WindowInsets.Consumed;
                }

                v.SetPadding(insets.SystemWindowInsetLeft, insets.SystemWindowInsetTop,
                             insets.SystemWindowInsetRight, insets.SystemWindowInsetBottom);
                return insets;
            }
        }
    }
}
