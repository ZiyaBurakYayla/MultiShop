using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class _OrderSummaryComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;

        public _OrderSummaryComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _basketService.GetBasket();
            ViewBag.total = Math.Round(values.TotalPrice, 2);
            var totalPriceWithTax = values.TotalPrice + values.TotalPrice / 100 * 10;
            ViewBag.totalPriceWithTax = Math.Round(totalPriceWithTax, 2);
            ViewBag.CargoPrice = 15m;
            var totalPrice = totalPriceWithTax + 15;
            ViewBag.totalPrice = Math.Round(totalPrice, 2);
            var basketItems = values.BasketItems;
            return View(basketItems);
        }
    }
}
