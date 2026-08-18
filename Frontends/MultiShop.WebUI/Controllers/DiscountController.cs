using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            var values = await _discountService.GetDiscountCouponRate(code);
            var basketValues = await _basketService.GetBasket();
            var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10;
            var totalNewPriceWithDiscount = (totalPriceWithTax - (totalPriceWithTax / 100 * values));
            ViewBag.totalNewPriceWithDiscount = Math.Round(totalNewPriceWithDiscount, 2); ;


            return RedirectToAction("Index","ShoppingCart",new {code = code,discountRate = values,
                totalNewPriceWithDiscount = totalNewPriceWithDiscount
            }); 
        }
    }
}
