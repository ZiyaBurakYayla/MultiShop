using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [Route("Seller/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;

        public ProductDetailController(IProductDetailService productDetailService, IProductService productService, ISellerService sellerService, ILoginService loginService)
        {
            _productDetailService = productDetailService;
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

        [Route("UpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)
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
            var values = await _productDetailService.GetByProductIdProductDetailAsync(id);
            if (values == null)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.productName = product.ProductName;
            return View(values);
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetail)
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            if (updateProductDetail == null)
            {
                return RedirectToAction("Index", "Product");
            }
            var product = await _productService.GetProductByIdAsync(updateProductDetail.ProductId);
            if (product == null || product.SellerId != seller.SellerId)
            {
                return RedirectToAction("Index", "Product");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.productName = product.ProductName;
                return View(updateProductDetail);
            }
            await _productDetailService.UpdateProductDetailAsync(updateProductDetail);
            return RedirectToAction("Index", "Product");
        }
    }
}
