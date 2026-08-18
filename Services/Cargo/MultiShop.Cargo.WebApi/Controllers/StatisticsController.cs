using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.WebApi.Services.StatisticServices;

namespace MultiShop.Cargo.WebApi.Controllers
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

        [HttpGet("GetTotalCargoCustomerCount")]
        public async Task<IActionResult> GetTotalCargoCustomerCount()
        {
            return Ok(await _statisticService.GetTotalCargoCustomerCountAsync());
        }
    }
}
