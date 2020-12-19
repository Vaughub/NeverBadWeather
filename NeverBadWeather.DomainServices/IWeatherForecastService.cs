using System.Collections.Generic;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;

namespace NeverBadWeather.DomainServices
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast> GetWeatherForecast(Place place);
        IEnumerable<Place> GetAllPlaces();
    }
}
