using System.IO;

namespace NeverBadWeather.Infrastructure.WeatherForecastService.Properties
{
	public static class PlaceData
	{
		public static string[] GetPlaceInfo()
		{
			return File.ReadAllLines(@"Properties\PlaceData.txt");
		} 
	}
}