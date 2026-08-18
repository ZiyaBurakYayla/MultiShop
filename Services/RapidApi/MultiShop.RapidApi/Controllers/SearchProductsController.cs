using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApi.Requests.CatalogRequests.CatalogProductRequests;
using MultiShop.RapidApi.Services.CatalogServices.CatalogProductServices;

namespace MultiShop.RapidApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] ProductSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ProductName))
                return BadRequest("Ürün adı belirtilmelidir.");

            var products = await _productService.SearchAsync(request);

            return Ok(products);
        }
    }
}