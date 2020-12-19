using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeverBadWeather.ApplicationServices;
using NeverBadWeather.UserInterfaceApi.Model;

namespace NeverBadWeather.UserInterfaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingRuleController : Controller
    {
        private readonly ClothingRecommendationService _service;

        public ClothingRuleController(ClothingRecommendationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClothingRule>>> GetAll()
        {
            var rulesDomain = await _service.GetRules(null);
            var rulesViewModel = rulesDomain.Select(ClothingRule.GetAsViewModel);
            return Ok(rulesViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrUpdate(ClothingRule clothingRule)
        {
            try
            {
                var ruleDomain = clothingRule.GetAsDomainModel();
                var result = await _service.CreateOrUpdateRule(ruleDomain);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Delete(ClothingRule clothingRule)
        {
            try
            {
                var ruleDomain = clothingRule.GetAsDomainModel();
                var result = await _service.DeleteRule(ruleDomain);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }
    }
}
