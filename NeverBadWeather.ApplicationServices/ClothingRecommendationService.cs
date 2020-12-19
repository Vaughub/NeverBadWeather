using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeverBadWeather.DomainModel;
using NeverBadWeather.DomainServices;

namespace NeverBadWeather.ApplicationServices
{
    public class ClothingRecommendationService
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly IClothingRuleRepository _clothingRuleRepository;

        public ClothingRecommendationService(IWeatherForecastService weatherForecastService, IClothingRuleRepository clothingRuleRepository)
        {
            _clothingRuleRepository = clothingRuleRepository;
            _weatherForecastService = weatherForecastService;
        }

        public async Task<IEnumerable<ClothingRule>> GetRules(User user)
        {
            return await _clothingRuleRepository.GetRulesByUser(user?.Id);
        }

        public async Task<ClothingRecommendation> GetClothingRecommendation(ClothingRecommendationRequest request)
        {
            var rules = await _clothingRuleRepository.GetRulesByUser(request.User?.Id);
            if (rules == null) return null;
            var placeList = PlaceList.Instance;
            if (!placeList.IsLoaded)
            {
	            var places = _weatherForecastService.GetAllPlaces();
	            placeList.Load(places);
	        }
            var place = placeList.GetClosestPlace(request.Location);
            var weatherForecast = await _weatherForecastService.GetWeatherForecast(place);
            weatherForecast.LimitTo(request.Time.From, request.Time.To);
            var stats = weatherForecast.GetStats();
            
            return new ClothingRecommendation(rules.Where(rule => rule.Match(stats)), weatherForecast, place);
        }

        public async Task<bool> CreateOrUpdateRule(ClothingRule rule)
        {
            var rowsAffected = await _clothingRuleRepository.Update(rule);
            if (rowsAffected == 0)
            {
                rowsAffected = await _clothingRuleRepository.Create(rule);
            }

            return rowsAffected == 1;
        }

        public async Task<bool> DeleteRule(ClothingRule rule)
        {
            var rowsAffected = await _clothingRuleRepository.Delete(rule);
            
            return rowsAffected == 1;
        }
    }
}
