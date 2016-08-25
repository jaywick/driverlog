using DriverLog.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DriverLog.Pages
{
    public partial class TripPage : ContentPage, INotifyPropertyChanged
    {
        INotification _tripNotification;
        int _tripID;
        DateTime? _startTime;
        bool _isDriving;

        // for debug only
        int _debugHourOffset;

        public TimeSpan Elapsed
        {
            get
            {
                if (!_startTime.HasValue)
                    return TimeSpan.Zero;

                return DateTime.UtcNow - _startTime.Value + TimeSpan.FromHours(_debugHourOffset);
            }
        }

        public TripPage()
        {
            BindingContext = this;
            InitializeComponent();

            StartTrip();
        }

        public async void StartTrip()
        {
            _isDriving = true;
            _startTime = DateTime.UtcNow;

            _tripID = App.Current.Store.CreateTrip(100000, false, WeatherStates.Sunny);
            _tripNotification = await App.Current.Notifier.Notify("DriverLog", $"{App.Current.Supervisor} has started supervising a driving lesson.");

            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateUI);
        }

        public async void Finish(object sender, EventArgs e)
        {
            _isDriving = false;

            App.Current.Store.FinishTrip(_tripID, 100020, _debugHourOffset);
            _tripNotification.Dismiss();

            await DisplayAlert("TODO", "Go to summary screen", "ok");
            await App.Current.Storyboard.Return();
        }

        public void DebugAddHour(object sender, EventArgs e)
        {
            _debugHourOffset++;
        }

        private bool UpdateUI()
        {
            OnPropertyChanged(nameof(Elapsed));

            // keep running if still driving
            return _isDriving;
        }
    }
}
