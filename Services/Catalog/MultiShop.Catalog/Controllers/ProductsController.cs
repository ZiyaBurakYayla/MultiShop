using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto product)
        {
            await _productService.CreateProductAsync(product);
            return Ok("Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto product)
        {
            await _productService.UpdateProductAsync(product);
            return Ok("Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Başarılı");
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductWithCategoryAsync();
            return Ok(values);
        }
        [HttpGet("ProductListWithCategoryByCategoryId/{id}")]
        public async Task<IActionResult> ProductListWithCategoryByCategoryId(string id)
        {
            var values = await _productService.GetProductWithCategoryByCategoryIdAsync(id);
            return Ok(values);
        }
        [HttpGet("ProductListBySellerId/{id}")]
        public async Task<IActionResult> ProductListBySellerId(string id)
        {
            var values = await _productService.GetProductsBySellerIdAsync(id);
            return Ok(values);
        }
    }
}
