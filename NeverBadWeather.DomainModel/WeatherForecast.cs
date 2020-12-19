using System;
using System.Collections.Generic;
using System.Linq;

namespace NeverBadWeather.DomainModel
{
    public class WeatherForecast
    {
        public TemperatureForecast[] Temperatures { get; private set; }

        public WeatherForecast(IEnumerable<TemperatureForecast> temperatures)
        {
            Temperatures = temperatures.ToArray();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Temperatures.Select(t => t.ToString()));
        }

        public void LimitTo(DateTime from, DateTime to)
        {
            Temperatures = Temperatures.Where(t =>
                IsBetween(t.FromTime, from, to) || IsBetween(t.FromTime, from, to)
            ).ToArray();
        }

        private static bool IsBetween(DateTime d, DateTime from, DateTime to)
        {
            return d >= from && d <= to;
        }

        public TemperatureStatistics GetStats()
        {
            var stats = new TemperatureStatistics();
            float rainAmount = 0;
            foreach (var temperature in Temperatures)
            {
                stats.AddTemperature(temperature.Temperature);
                rainAmount += temperature.RainAmount ?? 0;
            }
            
            stats.IsItRaining(rainAmount);
            
            return stats;
        }
    }
}
