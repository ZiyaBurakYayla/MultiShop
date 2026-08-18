using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;

namespace MultiShop.WebUI.Controllers
{
    public class StoreController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IProductService _productService;

        public StoreController(ISellerService sellerService, IProductService productService)
        {
            _sellerService = sellerService;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var seller = await _sellerService.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return RedirectToAction("Index", "Default");
            }

            var products = await _productService.GetProductsBySellerIdAsync(id);

            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Mağazalar";
            ViewBag.directory3 = seller.StoreName;

            var model = new StoreViewModel
            {
                Seller = seller,
                Products = products
            };
            return View(model);
        }
    }
}
