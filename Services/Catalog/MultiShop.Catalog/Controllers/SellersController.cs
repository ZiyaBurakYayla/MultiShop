using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SellerDtos;
using MultiShop.Catalog.Services.SellerServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellersController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSellers()
        {
            var values = await _sellerService.GetAllSellerAsync();
            return Ok(values);
        }

        [HttpGet("SellerListByStatus/{status}")]
        public async Task<IActionResult> GetSellersByStatus(string status)
        {
            var values = await _sellerService.GetSellersByStatusAsync(status);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSellerById(string id)
        {
            var value = await _sellerService.GetSellerByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller(CreateSellerDto createSellerDto)
        {
            await _sellerService.CreateSellerAsync(createSellerDto);
            return Ok("Satıcı başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSeller(UpdateSellerDto updateSellerDto)
        {
            await _sellerService.UpdateSellerAsync(updateSellerDto);
            return Ok("Satıcı başarıyla güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSeller(string id)
        {
            await _sellerService.DeleteSellerAsync(id);
            return Ok("Satıcı başarıyla silindi");
        }
    }
}
