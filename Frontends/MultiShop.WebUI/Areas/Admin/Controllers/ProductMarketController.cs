using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.RapidApiDtos.CatalogProductDtos;
using MultiShop.WebUI.Models.Requests;
using MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ProductMarket")]
    public class ProductMarketController : Controller
    {
        private readonly IProductSearchService _productSearchService;

        public ProductMarketController(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string productName)
        {
            ViewBag.productName = productName;

            if (string.IsNullOrWhiteSpace(productName))
            {
                return View(new List<ProductSearchDto>());
            }

            var values = await _productSearchService.SearchAsync(new ProductSearchRequest { ProductName = productName });
            return View(values);
        }
    }
}
