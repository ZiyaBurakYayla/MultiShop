using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductDetails()
        {
            var productDetails = await _productDetailService.GetAllProductDetailsAsync();
            return Ok(productDetails);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var productDetail = await _productDetailService.GetProductDetailByIdAsync(id);
            return Ok(productDetail);
        }
        [HttpGet("GetProductDetailByProductId/{id}")]
        public async Task<IActionResult> GetProductDetailByProductId(string id)
        {
            var productDetail = await _productDetailService.GetByProductIdProductDetailAsync(id);
            return Ok(productDetail);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto productDetail)
        {
            await _productDetailService.CreateProductDetailAsync(productDetail);
            return Ok("Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto productDetail)
        {
            await _productDetailService.UpdateProductDetailAsync(productDetail);
            return Ok("Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("Başarılı");
        }
    }
}
