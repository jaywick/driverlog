using SQLite;
using System;

namespace DriverLog.Data
{
    public class TripRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Supervisor { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finished { get; set; }
        public int WeatherID { get; set; }
        public double OdometerStart { get; set; }
        public double? OdometerFinal { get; set; }
        public bool IsNightTime { get; set; }

        //public GpsCoords[] Tracks { get; set; }
        //public double DayHoursElapsed { get; set; }
        //public double NightHoursElapsed { get; set; }
        //public double TotalKilometers { get; set; }
    }
}