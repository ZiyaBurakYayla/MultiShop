using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.Requests;
using MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices;

namespace MultiShop.WebUI.Controllers
{
    public class ProductSearchController : Controller
    {
        private readonly IProductSearchService _productSearchService;

        public ProductSearchController(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        public async Task<IActionResult> Index(ProductSearchRequest productName)
        {
            var values = await _productSearchService.SearchAsync(productName);
            return View(values);
        }

    }
}
