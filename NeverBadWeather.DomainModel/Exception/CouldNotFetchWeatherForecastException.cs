using System;

namespace NeverBadWeather.DomainModel.Exception
{
    public class CouldNotFetchWeatherForecastException : ApplicationException
    {
        public CouldNotFetchWeatherForecastException(System.Exception innerException)
        : base("", innerException)
        {
        }
    }
}
