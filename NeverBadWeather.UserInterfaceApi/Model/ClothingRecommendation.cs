using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeverBadWeather.UserInterfaceApi.Model
{
    public class ClothingRecommendation
    {
        public IEnumerable<ClothingRule> Rules { get; }
        public IEnumerable<string> WeatherForecast { get; }
        public string Place { get; }

        public ClothingRecommendation(DomainModel.ClothingRecommendation recommendation)
        {
            Rules = recommendation.Rules.Select(ClothingRule.GetAsViewModel);
            Place = recommendation.Place.ToString();
            WeatherForecast = recommendation.WeatherForecast.Temperatures.Select(t => t.ToString());

        }
    }
}
