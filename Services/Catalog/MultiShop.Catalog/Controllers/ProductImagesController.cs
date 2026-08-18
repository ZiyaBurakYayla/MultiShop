using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductImages()
        {
            var productImages = await _productImageService.GetAllProductImagesAsync();
            return Ok(productImages);
        }

        [HttpGet("ProductImageByProductId")]
        public async Task<IActionResult> GetAllProductImages(string id)
        {
            var productImage = await _productImageService.GetByProductIdProductImageAsync(id);
            return Ok(productImage);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            var productImage = await _productImageService.GetProductImageByIdAsync(id);
            return Ok(productImage);
        }

        [HttpGet("ProductImagesByProductId/{id}")]
        public async Task<IActionResult> ProductImagesByProductId(string id)
        {
            var productImage = await _productImageService.GetByProductIdProductImageAsync(id);
            return Ok(productImage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto productImage)
        {
            await _productImageService.CreateProductImageAsync(productImage);
            return Ok("Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto productImage)
        {
            await _productImageService.UpdateProductImageAsync(productImage);
            return Ok("Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await _productImageService.DeleteProductImageAsync(id);
            return Ok("Başarılı");
        }
    }
}
