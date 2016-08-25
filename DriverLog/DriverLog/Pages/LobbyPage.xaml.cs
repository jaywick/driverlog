using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DriverLog.Pages
{
    public partial class LobbyPage : ContentPage, INotifyPropertyChanged
    {
        public bool IsLoggedIn { get; set; }

        public LobbyPage()
        {
            // link {Binding x} properties to this class
            BindingContext = this;

            // load the XAML up
            InitializeComponent();
        }

        // called when Page visually appears
        protected override void OnAppearing()
        {
            OnPropertyChanged(nameof(DayHoursPercent));
        }

        public string Supervisor
        {
            get { return App.Current.Supervisor; }
            set { App.Current.Supervisor = value; }
        }

        public double DayHoursPercent
        {
            get
            {
                var hours = App.Current.Store?.GetDaylightHours() ?? 0; // get hours, if store null, return 0
                return hours / 120;
            }
        }

        public double NightHoursPercent
        {
            get
            {
                var hours = App.Current.Store?.GetNightHours() ?? 0; // get hours, if store null, return 0
                return hours / 20;
            }
        }

        public double LearningGoalsPercent
        {
            get { return 0.5; }
        }

        public async void Login(object sender, EventArgs e)
        {
            // open SupervisorLoginPage, await response from user
            var supervisor = await App.Current.Storyboard.PromptAsync(new SupervisorLoginPage());

            IsLoggedIn = true;
            Supervisor = supervisor;

            // notify UI of updated items
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(Supervisor));
        }

        public async void Start(object sender, EventArgs e)
        {
            await App.Current.Storyboard.ShowAsync(new TripPage());
        }
    }
}
