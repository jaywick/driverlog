using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace DriverLog.Droid
{
    /// <summary>
    /// Custom activity added to Droid project to show up first and display a logo as there's a noticable delay launching Android apps
    /// </summary>
    [Activity(Label = "DriverLog", Icon = "@drawable/ic_launcher", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        #region Protected Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StaticContext.Current = this;

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        #endregion Protected Methods
    }
}

