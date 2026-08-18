using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [Route("Seller/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;

        public ProductImageController(IProductImageService productImageService, IProductService productService, ISellerService sellerService, ILoginService loginService)
        {
            _productImageService = productImageService;
            _productService = productService;
            _sellerService = sellerService;
            _loginService = loginService;
        }

        private async Task<ResultSellerDto> GetCurrentSeller()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            if (sellers == null)
            {
                return null;
            }
            return sellers.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId && x.Status == "Approved");
        }

        [Route("UpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null || product.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index", "Product");
            }
            var values = await _productImageService.GetByProductIdProductImageAsync(id);
            if (values == null)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.productName = product.ProductName;
            return View(values);
        }

        [Route("UpdateProductImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (updateProductImageDto == null)
            {
                return RedirectToAction("Index", "Product");
            }
            var product = await _productService.GetProductByIdAsync(updateProductImageDto.ProductId);
            if (product == null || product.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index", "Product");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.productName = product.ProductName;
                return View(updateProductImageDto);
            }
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("Index", "Product");
        }
    }
}
