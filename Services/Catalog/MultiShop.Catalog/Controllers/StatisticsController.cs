using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.StatisticServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("GetBrandCount")]
        public async Task<IActionResult> GetBrandCount()
        {
            return Ok(await _statisticService.GetBrandCountAsync());
        }

        [HttpGet("GetProductCount")]
        public async Task<IActionResult> GetProductCount()
        {
            return Ok(await _statisticService.GetProductCountAsync());
        }

        [HttpGet("GetCategoryCount")]
        public async Task<IActionResult> GetCategoryCount()
        {
            return Ok(await _statisticService.GetCategoryCountAsync());
        }

        [HttpGet("GetProductAvgPrice")]
        public async Task<IActionResult> GetProductAvgPrice()
        {
            return Ok(await _statisticService.GetProductAvgPriceAsync());
        }

        [HttpGet("GetMaxPriceProductName")]
        public async Task<IActionResult> GetMaxPriceProductName()
        {
            return Ok(await _statisticService.GetMaxPriceProductNameAsync());
        }

        [HttpGet("GetMinPriceProductName")]
        public async Task<IActionResult> GetMinPriceProductName()
        {
            return Ok(await _statisticService.GetMinPriceProductNameAsync());
        }

        [HttpGet("GetProductImageCount")]
        public async Task<IActionResult> GetProductImageCount()
        {
            return Ok(await _statisticService.GetProductImageCountAsync());
        }

        [HttpGet("GetSpecialOfferCount")]
        public async Task<IActionResult> GetSpecialOfferCount()
        {
            return Ok(await _statisticService.GetSpecialOfferCountAsync());
        }

        [HttpGet("GetFeatureCount")]
        public async Task<IActionResult> GetFeatureCount()
        {
            return Ok(await _statisticService.GetFeatureCountAsync());
        }

        [HttpGet("GetLastBrandName")]
        public async Task<IActionResult> GetLastBrandName()
        {
            return Ok(await _statisticService.GetLastBrandNameAsync());
        }

        [HttpGet("GetLastProductName")]
        public async Task<IActionResult> GetLastProductName()
        {
            return Ok(await _statisticService.GetLastProductNameAsync());
        }
    }
}
