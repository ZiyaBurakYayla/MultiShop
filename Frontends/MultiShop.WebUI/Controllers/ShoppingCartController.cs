using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShoppingCartController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }

        public async Task <IActionResult> Index(string code,int discountRate,decimal totalNewPriceWithDiscount)
        {
            ViewBag.Code = code;
            ViewBag.DiscountRate = discountRate;
            ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            var values = await _basketService.GetBasket();
            ViewBag.total = Math.Round(values.TotalPrice, 2);
            var totalPriceWithTax = values.TotalPrice + values.TotalPrice / 100 * 10;
            ViewBag.totalPriceWithTax = Math.Round(totalPriceWithTax, 2); ;
            var tax = values.TotalPrice / 100 * 10;
            ViewBag.tax = Math.Round(tax, 2); ;

            return View();
        }

        public async Task<IActionResult> AddBasketItem(string id, string sku, int quantity)
        {
            var value = await _productService.GetProductByIdAsync(id);
            if (quantity < 1)
            {
                quantity = 1;
            }
            var price = value.ProductPrice;
            if (!string.IsNullOrEmpty(sku) && value.Variants != null)
            {
                foreach (var variant in value.Variants)
                {
                    if (variant.Sku == sku)
                    {
                        price = variant.Price;
                    }
                }
            }
            var items = new BasketItemDto
            {
                ProductId = value.ProductId,
                ProductName = value.ProductName,
                Price = price,
                Quantity = quantity,
                ProductImageUrl = value.ProductImageUrl,
                Sku = string.IsNullOrEmpty(sku) ? "" : sku,
                SellerId = string.IsNullOrEmpty(value.SellerId) ? "" : value.SellerId
            };
            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string productId, int quantity)
        {
            if (quantity < 0)
            {
                quantity = 0;
            }
            var basket = await _basketService.UpdateBasketItemQuantity(productId, quantity);
            var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var total = Math.Round(basket.TotalPrice, 2);
            var tax = Math.Round(basket.TotalPrice / 100 * 10, 2);
            var totalPriceWithTax = Math.Round(basket.TotalPrice + basket.TotalPrice / 100 * 10, 2);

            return Json(new
            {
                removed = item == null,
                quantity = item == null ? 0 : item.Quantity,
                lineTotal = item == null ? 0 : Math.Round(item.Quantity * item.Price, 2),
                total = total,
                tax = tax,
                totalPriceWithTax = totalPriceWithTax,
                itemCount = basket.BasketItems.Count
            });
        }

        public async Task<IActionResult> DiscardBasketItem(string id)
        {
            await _basketService.DiscardBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
