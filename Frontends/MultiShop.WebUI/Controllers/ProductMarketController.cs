using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.RapidApiDtos.CatalogProductDtos;
using MultiShop.WebUI.Models.Requests;
using MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices;

namespace MultiShop.WebUI.Controllers
{
    public class ProductMarketController : Controller
    {
        private readonly IProductSearchService _productSearchService;

        public ProductMarketController(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        public async Task<IActionResult> Index(string productName)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Pazarı";
            ViewBag.productName = productName;

            if (string.IsNullOrWhiteSpace(productName))
            {
                return View(new List<ProductSearchDto>());
            }

            var request = new ProductSearchRequest { ProductName = productName };
            var values = await _productSearchService.SearchAsync(request);
            return View(values);
        }
    }
}
