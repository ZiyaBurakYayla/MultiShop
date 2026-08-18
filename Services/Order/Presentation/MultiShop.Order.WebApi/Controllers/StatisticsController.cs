using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.WebApi.Services.StatisticServices;

namespace MultiShop.Order.WebApi.Controllers
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

        [HttpGet("GetTotalOrderCount")]
        public async Task<IActionResult> GetTotalOrderCount()
        {
            return Ok(await _statisticService.GetTotalOrderCountAsync());
        }

        [HttpGet("GetTotalOrderDetailCount")]
        public async Task<IActionResult> GetTotalOrderDetailCount()
        {
            return Ok(await _statisticService.GetTotalOrderDetailCountAsync());
        }
    }
}
