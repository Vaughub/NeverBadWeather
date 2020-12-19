using System;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;
using NeverBadWeather.Infrastructure.WeatherForecastService;

namespace NeverBadWeatcher.UserInterfaceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var service = new WeatherForecastServiceYr();
            var places = service.GetAllPlaces();
            var placeList = PlaceList.Instance;
            placeList.Load(places);

            var location = new Location(59.00474f, 10.02773f);
            var place = placeList.GetClosestPlace(location);
            var weatherForecast = await service.GetWeatherForecast(place);
            Console.WriteLine(weatherForecast);
        }
    }
}
