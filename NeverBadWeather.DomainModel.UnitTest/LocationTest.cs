using System;
using NUnit.Framework;

namespace NeverBadWeather.DomainModel.UnitTest
{
    public class LocationTest
    {
        [Test]
        public void TestIsWithinInside()
        {
            // arrange
            var corner1 = new Location(0, 0);
            var corner2 = new Location(1, 1);
            var location = new Location(0.5f, 0.5f);

            // act
            var isWithin = location.IsWithin(corner1, corner2);

            // assert
            Assert.IsTrue(isWithin);
        }

        [Test]
        public void TestIsWithinOutside()
        {
	        var corner1 = new Location(0, 0);
	        var corner2 = new Location(1, 1);
	        var location = new Location(1.5f, 1.5f);

	        var isWithin = location.IsWithin(corner1, corner2);

            Assert.IsFalse(isWithin);
        }

        [Test]
        public void WeatherForecastLimitToTest()
        {
	        var date = new DateTime(2000, 1, 1, 10, 0, 0);

	        var forecast = new WeatherForecast(new TemperatureForecast[]
	        {
		        new TemperatureForecast(20, 0, date.AddHours(1), date.AddHours(2)),
		        new TemperatureForecast(22, 0, date.AddHours(2), date.AddHours(3)),
		        new TemperatureForecast(25, 0, date.AddHours(3), date.AddHours(4)),
	        });

	        forecast.LimitTo(date, date.AddMinutes(61));

            Assert.AreEqual(1, forecast.Temperatures.Length);
        }
    }
}