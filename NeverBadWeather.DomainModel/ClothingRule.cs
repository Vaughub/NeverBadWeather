using System;

namespace NeverBadWeather.DomainModel
{
    public class ClothingRule : BaseEntity
    {
        public int FromTemperature { get; }
        public int ToTemperature { get; }
        public bool? IsRaining { get; }
        public string Clothes { get; }

        public bool Match(float temperature)
        {
            return temperature >= FromTemperature && temperature <= ToTemperature;
        }

        public bool Match(TemperatureStatistics stats)
        {
	        
	        if (IsRaining != null) return (Match(stats.Min) || Match(stats.Max)) && IsRaining == stats.Raining;
	        
	        return Match(stats.Min) || Match(stats.Max);
        }

        public ClothingRule(int fromTemperature, int toTemperature, bool? isRaining, string clothes, Guid? id = null)
            : base(id)
        {
            FromTemperature = fromTemperature;
            ToTemperature = toTemperature;
            IsRaining = isRaining;
            Clothes = clothes;
        }
    }
}
