using DriverLog.Data;
using DriverLog.Pages;

using Xamarin.Forms;

namespace DriverLog
{
    /// <summary>
    /// Class of global scope, everything is here
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Manages navigation between Pages
        /// </summary>
        public Storyboard Storyboard { get; private set; }

        /// <summary>
        /// Wrapper to handle database
        /// </summary>
        public Store Store { get; private set; }

        /// <summary>
        /// Platform specific notifications
        /// </summary>
        public INotifier Notifier { get; private set; }

        /// <summary>
        /// Supervisor currently logged in
        /// </summary>
        public string Supervisor { get; set; }

        /// <summary>
        /// Replaces the <code>Application.Current</code> property which isn't of type App
        /// </summary>
        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        /// <summary>
        /// Application entry point, it all starts here
        /// </summary>
        public App()
        {
            Storyboard = new Storyboard(this);
            Storyboard.Start(new LobbyPage());

            Store = new Store(this);
            Store.ResetForDebugging(); // clear everything and start again (obv for debug/testing)

            Notifier = GetPlatformImplementation<INotifier>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// Gets a platform specific implementation of a class such as SQLite or Notifications.
        /// </summary>
        public static T GetPlatformImplementation<T>() where T : class
        {
            var implementation = DependencyService.Get<T>();

            if (implementation == null)
                throw new System.Exception($"Could not resolve platform specific implementation of {typeof(T).Name}");

            return implementation;
        }
    }
}
