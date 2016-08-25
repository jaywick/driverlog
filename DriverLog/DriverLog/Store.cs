using DriverLog.Data;
using SQLite;
using System.Linq;
using System;

namespace DriverLog
{
    public class Store
    {
        private static readonly string DBNAME = "database.db3";

        private App _app;
        private SQLiteConnection _database;

        public bool IsReady { get; private set; }

        public Store(App app)
        {
            _app = app;

            var sqlite = App.GetPlatformImplementation<ISQLite>();
            _database = sqlite.Connect(DBNAME);

            IsReady = true;
        }

        public void ResetForDebugging()
        {
#warning THIS SHOULD BE REMOVED IN PRODUCTION CODE OBVIOUSLY

            _database.DropTable<TripRecord>();
            _database.CreateTable<TripRecord>();
        }

        public int CreateTrip(int odometer, bool isNightTime, WeatherStates weather)
        {
            return _database.Insert(new TripRecord
            {
                Start = DateTime.UtcNow,
                Supervisor = App.Current.Supervisor,
                OdometerStart = odometer,
                IsNightTime = isNightTime,
                WeatherID = (int)weather,
            });
        }

        public void FinishTrip(int tripID, int odometer, int DEBUGADDHOURS = 0)
        {
            var trip = _database.Find<TripRecord>(tripID);
            trip.Finished = DateTime.UtcNow.AddHours(DEBUGADDHOURS);
            trip.OdometerFinal = odometer;
            _database.Update(trip);
        }

        public double GetDaylightHours()
        {
            return _database.Table<TripRecord>()
                .Where(x => !x.IsNightTime && x.Finished != null)
                .ToList() // this forces data to load from db
                .Sum(x => (x.Finished.Value - x.Start).TotalHours);
        }

        public double GetNightHours()
        {
            return _database
                .Table<TripRecord>()
                .Where(x => x.IsNightTime && x.Finished != null)
                .ToList() // materialise results to list
                .Sum(x => (x.Finished.Value - x.Start).TotalHours);
        }
    }
}