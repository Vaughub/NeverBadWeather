using System;

namespace NeverBadWeather.DomainModel
{
    public class TemperatureForecast
    {
        public float Temperature { get; }
        public float? RainAmount { get; set; }
        public DateTime FromTime { get; }
        public DateTime ToTime { get; }

        public TemperatureForecast(float temperature, float? rainAmount, DateTime fromTime, DateTime time)
        {
            Temperature = temperature;
            RainAmount = rainAmount;
            FromTime = fromTime;
            ToTime = time;
        }

        public override string ToString()
        {
            return $"{Temperature}°C fra {FromTime:g} til {ToTime:t}";
        }
    }
}
