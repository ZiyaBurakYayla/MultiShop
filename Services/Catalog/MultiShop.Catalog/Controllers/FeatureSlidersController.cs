using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSlidersController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeatureSliders()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id)
        {
            var value = await _featureSliderService.GetFeatureSliderByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("Feature Slider başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Feature Slider başarıyla güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Feature Slider başarıyla silindi");
        }

        [HttpGet("FeatureSliderChangeStatusToTrue/{id}")]
        public async Task<IActionResult> FeatureSliderChangeStatusToTrue(string id)
        {
            await _featureSliderService.FeatureSliderChangeStatusToTrue(id);
            return Ok("Feature Slider durumu aktif edildi");
        }

        [HttpGet("FeatureSliderChangeStatusToFalse/{id}")]
        public async Task<IActionResult> FeatureSliderChangeStatusToFalse(string id)
        {
            await _featureSliderService.FeatureSliderChangeStatusToFalse(id);
            return Ok("Feature Slider durumu pasif edildi");
        }
    }
}
