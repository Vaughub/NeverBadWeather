using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;
using NeverBadWeather.DomainServices;
using NeverBadWeather.Infrastructure.WeatherForecastService.Model;
using NeverBadWeather.Infrastructure.WeatherForecastService.Properties;
using RestSharp;

namespace NeverBadWeather.Infrastructure.WeatherForecastService
{
    public class WeatherForecastServiceYr : IWeatherForecastService
    {
	    public async Task<WeatherForecast> GetWeatherForecast(Place place)
        {
	        var request = new RestRequest("compact");
	        request.AddQueryParameter("lat", place.Location.Latitude.ToString(CultureInfo.InvariantCulture));
	        request.AddQueryParameter("lon", place.Location.Longitude.ToString(CultureInfo.InvariantCulture));

	        var client = new RestClient("https://api.met.no/weatherapi/locationforecast/2.0/");

	        var response = await client.ExecuteGetAsync<MetaForecast>(request);

	        var forecasts = response.Data.Properties.Timeseries.Select(f => 
			        new TemperatureForecast(f.Data.Instant.Details.AirTemperature, f.Data.Next_1_Hours?.Details.PrecipitationAmount,
				        DateTime.Parse(f.Time), DateTime.Parse(f.Time).AddHours(1)));

	        return new WeatherForecast(forecasts);
        }

        //private static TemperatureForecast TemperatureForecastFromXml(weatherdataForecastTime data)
        //{
        //    var temperature = data.temperature.value;
        //    return new TemperatureForecast(temperature, data.from, data.to);
        //}

        //public IEnumerable<Place> GetAllPlaces()
        //{
        //    var lines = Resources.noreg
        //                         .Split(
        //                    Environment.NewLine.ToCharArray(), 
        //                            StringSplitOptions.RemoveEmptyEntries)
        //                         .Skip(1);
        //    return lines.Select(PlaceFromCsvLine).ToList();
        //}

        public IEnumerable<Place> GetAllPlaces()
        {
	        return PlaceData.GetPlaceInfo().Skip(1).Select(p => p.Split("\t"))
		        .Select(s => new Place("Norge", s[7], s[6], s[1], new Location(s[8], s[9])));
        }

        //private static Place PlaceFromCsvLine(string line)
        //{
        //    var fields = line.Split('\t');
        //    var location = new Location(fields[8], fields[9]);
            
        //    return new Place("Norge", fields[7], fields[6], fields[1], location);
        //}
    }
}
