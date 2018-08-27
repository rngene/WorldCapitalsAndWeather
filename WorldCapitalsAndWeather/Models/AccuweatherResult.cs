using System;

namespace WorldCapitalsAndWeather.Models
{
    public class AccuweatherResult
    {
        public Temperature Temperature { get; set; }
    }

    public class Temperature
    {
        public TemperatureValue Metric { get; set; }
        public TemperatureValue Imperial { get; set; }
    }

    public class TemperatureValue
    {
        public Decimal Value { get; set; }
        public string Unit { get; set; }
        public string UnitType { get; set; }
    }
}
