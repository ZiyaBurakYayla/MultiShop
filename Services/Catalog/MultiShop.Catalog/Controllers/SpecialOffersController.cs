using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialOffers()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            var value = await _specialOfferService.GetSpecialOfferByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Special Offer başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Special Offer başarıyla güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Special Offer başarıyla silindi");
        }

        [HttpGet("SpecialOfferChangeStatusToTrue/{id}")]
        public async Task<IActionResult> SpecialOfferChangeStatusToTrue(string id)
        {
            await _specialOfferService.SpecialOfferChangeStatusToTrue(id);
            return Ok("Special Offer durumu aktif edildi");
        }

        [HttpGet("SpecialOfferChangeStatusToFalse/{id}")]
        public async Task<IActionResult> SpecialOfferChangeStatusToFalse(string id)
        {
            await _specialOfferService.SpecialOfferChangeStatusToFalse(id);
            return Ok("Special Offer durumu pasif edildi");
        }
    }
}
