using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NeverBadWeather.ApplicationServices;
using NeverBadWeather.DomainModel;
using NeverBadWeather.DomainServices;
using NeverBadWeather.Infrastructure.WeatherForecastService;
using NUnit.Framework;

namespace NeverBadWeather.ApplicationService.UnitTest
{
    public class ClothingRecommendationServiceTest
    {

        [Test]
        public async Task TestHappyCase()
        {
            // arrange
            var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
            var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
            var testLocation = new Location(59, 10);

            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

            mockClothingRuleRepository
                .Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
                .ReturnsAsync(new[]
                {
                    new ClothingRule(-20, 10, null, "Bobledress"),
                    new ClothingRule(10, 20, null, "Bukse og genser"),
                    new ClothingRule(20, 40, null, "T-skjore og shorts"),
                });

            mockWeatherForecastService
                .Setup(fs => fs.GetAllPlaces())
                .Returns(new[] { new Place("", "", "", "Andeby", new Location(59.1f, 10.1f)), });

            mockWeatherForecastService
                .Setup(fs => fs.GetWeatherForecast(It.IsAny<Place>()))
                .ReturnsAsync(new WeatherForecast(new[] {
                    new TemperatureForecast(25, 0, testDate.AddHours(2), testDate.AddHours(4)),
                }));

            // act
            var request = new ClothingRecommendationRequest(testPeriod, testLocation);
            var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockClothingRuleRepository.Object);
            var recommendation = await service.GetClothingRecommendation(request);

            // assert
            Assert.AreEqual("Andeby",recommendation.Place.Name);
            Assert.That(recommendation.Rules, Has.Exactly(1).Items);
            var rule = recommendation.Rules.First();
            Assert.AreEqual("T-skjore og shorts", rule.Clothes);
        }

        [Test]
        public async Task NoWeatherReportTest()
        {
	        var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
	        var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
	        var testLocation = new Location(59, 10);

	        var mockWeatherForecastService = new Mock<IWeatherForecastService>();
	        var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

	        mockClothingRuleRepository.Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
		        .ReturnsAsync(null as IEnumerable<ClothingRule>);

	        var request = new ClothingRecommendationRequest(testPeriod, testLocation);
	        var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockClothingRuleRepository.Object);
	        
	        var recommendation = await service.GetClothingRecommendation(request);

			mockClothingRuleRepository.Verify(c => c.GetRulesByUser(It.IsAny<Guid?>()));
			mockClothingRuleRepository.VerifyNoOtherCalls();

			Assert.Null(recommendation);
        }

		[Test]
		public async Task NoWeatherForecastAtThatTemp()
		{
			var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
			var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
			var testLocation = new Location(59, 10);

			var mockWeatherForecastService = new Mock<IWeatherForecastService>();
			var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

			mockClothingRuleRepository.Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
				.ReturnsAsync(new[]
				{
					new ClothingRule(-20, 10, null, "Bobledress"),
					new ClothingRule(10, 20, null, "Bukse og genser"),
					new ClothingRule(20, 40, null, "T-skjore og shorts"),
				});

			mockWeatherForecastService
				.Setup(fs => fs.GetAllPlaces())
				.Returns(new[] { new Place("", "", "", "Andeby", new Location(59.1f, 10.1f)), });

			mockWeatherForecastService
				.Setup(fs => fs.GetWeatherForecast(It.IsAny<Place>()))
				.ReturnsAsync(new WeatherForecast(new[] {
					new TemperatureForecast(45, 0, testDate.AddHours(2), testDate.AddHours(4)),
				}));

			var request = new ClothingRecommendationRequest(testPeriod, testLocation);
			var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockClothingRuleRepository.Object);

			var recommendation = await service.GetClothingRecommendation(request);

			Assert.That(recommendation.Rules, Has.Exactly(0).Items);
		}

		[Test]
		public async Task RainingTest()
		{
			var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
			var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
			var testLocation = new Location(59, 10);

			var mockWeatherForecastService = new Mock<IWeatherForecastService>();
			var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

			mockClothingRuleRepository.Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
				.ReturnsAsync(new[]
				{
					new ClothingRule(-20, -5, null, "Bobledress"),
					new ClothingRule(-5, 10, true, "Regntøy"),
					new ClothingRule(10, 20, null, "Genser og bukse"),
					new ClothingRule(20, 40, null, "T-skjore og shorts"),
				});

			mockWeatherForecastService
				.Setup(fs => fs.GetAllPlaces())
				.Returns(new[] { new Place("", "", "", "Andeby", new Location(59.1f, 10.1f)), });

			mockWeatherForecastService
				.Setup(fs => fs.GetWeatherForecast(It.IsAny<Place>()))
				.ReturnsAsync(new WeatherForecast(new[] {
					new TemperatureForecast(5, 2, testDate.AddHours(2), testDate.AddHours(4)),
				}));

			var request = new ClothingRecommendationRequest(testPeriod, testLocation);
			var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockClothingRuleRepository.Object);

			var recommendation = await service.GetClothingRecommendation(request);

			Assert.That(recommendation.Rules, Has.Exactly(1).Items);
			Assert.IsTrue(recommendation.Rules.First().IsRaining);
			Assert.AreEqual("Regntøy", recommendation.Rules.First().Clothes);
		}

		[Test]
		public async Task Default()
		{
			var testDate = new DateTime(2000, 1, 1, 10, 0, 0);
			var testPeriod = new TimePeriod(testDate, testDate.AddHours(10));
			var testLocation = new Location(59, 10);

			var mockWeatherForecastService = new Mock<IWeatherForecastService>();
			var mockClothingRuleRepository = new Mock<IClothingRuleRepository>();

			mockClothingRuleRepository.Setup(crr => crr.GetRulesByUser(It.IsAny<Guid?>()))
				.ReturnsAsync(new[]
				{
					new ClothingRule(-20, 10, null, "Bobledress"),
					new ClothingRule(10, 20, null, "Bukse og genser"),
					new ClothingRule(20, 40, null, "T-skjore og shorts"),
				});

			mockWeatherForecastService
				.Setup(fs => fs.GetAllPlaces())
				.Returns(new[] { new Place("", "", "", "Andeby", new Location(59.1f, 10.1f)), });

			mockWeatherForecastService
				.Setup(fs => fs.GetWeatherForecast(It.IsAny<Place>()))
				.ReturnsAsync(new WeatherForecast(new[] {
					new TemperatureForecast(25, 0, testDate.AddHours(2), testDate.AddHours(4)),
				}));

			var request = new ClothingRecommendationRequest(testPeriod, testLocation);
			var service = new ClothingRecommendationService(mockWeatherForecastService.Object, mockClothingRuleRepository.Object);

			var recommendation = await service.GetClothingRecommendation(request);

			Assert.AreEqual("Andeby", recommendation.Place.Name);
			Assert.That(recommendation.Rules, Has.Exactly(1).Items);
			var rule = recommendation.Rules.First();
			Assert.AreEqual("T-skjore og shorts", rule.Clothes);
		}

		[Test]
        public async Task UpdateRuleTest()
        {
	        var clothingRuleMock = new Mock<IClothingRuleRepository>();
	        var weatherForecastMock = new Mock<IWeatherForecastService>();

	        clothingRuleMock.Setup(c => c.Update(It.IsAny<ClothingRule>())).ReturnsAsync(1);

	        var clothingRecommendation = new ClothingRecommendationService(weatherForecastMock.Object, clothingRuleMock.Object);

	        var response = await clothingRecommendation.CreateOrUpdateRule(It.IsAny<ClothingRule>());
            clothingRuleMock.Verify(c => c.Update(It.IsAny<ClothingRule>()));
            clothingRuleMock.VerifyNoOtherCalls();
            Assert.IsTrue(response);
        }

        [Test]
        public async Task CreateRuleTest()
        {
	        var clothingRuleMock = new Mock<IClothingRuleRepository>();
	        var weatherForecastMock = new Mock<IWeatherForecastService>();

	        clothingRuleMock.Setup(c => c.Update(It.IsAny<ClothingRule>())).ReturnsAsync(0);
	        clothingRuleMock.Setup(c => c.Create(It.IsAny<ClothingRule>())).ReturnsAsync(1);

	        var clothingRecommendation = new ClothingRecommendationService(weatherForecastMock.Object, clothingRuleMock.Object);

	        var response = await clothingRecommendation.CreateOrUpdateRule(It.IsAny<ClothingRule>());
	        clothingRuleMock.Verify(c => c.Update(It.IsAny<ClothingRule>()));
	        clothingRuleMock.Verify(c => c.Create(It.IsAny<ClothingRule>()));
	        clothingRuleMock.VerifyNoOtherCalls();
	        Assert.IsTrue(response);
        }

        [Test]
        public async Task UpdateRuleFailTest()
        {
	        var clothingRuleMock = new Mock<IClothingRuleRepository>();
	        var weatherForecastMock = new Mock<IWeatherForecastService>();

	        clothingRuleMock.Setup(c => c.Update(It.IsAny<ClothingRule>())).ReturnsAsync(0);
	        clothingRuleMock.Setup(c => c.Create(It.IsAny<ClothingRule>())).ReturnsAsync(0);

	        var clothingRecommendation = new ClothingRecommendationService(weatherForecastMock.Object, clothingRuleMock.Object);

			var response = await clothingRecommendation.CreateOrUpdateRule(It.IsAny<ClothingRule>());

	        Assert.IsFalse(response);
        }
    }
}