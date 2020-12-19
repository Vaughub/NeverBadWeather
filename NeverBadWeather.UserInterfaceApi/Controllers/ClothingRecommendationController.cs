using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeverBadWeather.ApplicationServices;
using NeverBadWeather.UserInterfaceApi.Model;

namespace NeverBadWeather.UserInterfaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingRecommendationController : ControllerBase
    {
        private readonly ClothingRecommendationService _service;

        public ClothingRecommendationController(ClothingRecommendationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ClothingRule>>> Get(ClothingRecommendationRequest request)
        {
            var clothingRecommendationRequest = request.ToDomainModel();
            var recommendation = await _service.GetClothingRecommendation(clothingRecommendationRequest);
            var viewModel = new ClothingRecommendation(recommendation);
            return Ok(viewModel);
        }
    }
}
