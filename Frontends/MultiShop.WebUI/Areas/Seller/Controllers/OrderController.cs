using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;

namespace MultiShop.WebUI.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [Route("Seller/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;

        public OrderController(IOrderDetailService orderDetailService, ISellerService sellerService, ILoginService loginService)
        {
            _orderDetailService = orderDetailService;
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

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var seller = await GetCurrentSeller();
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }
            var values = await _orderDetailService.GetOrderDetailsBySellerIdAsync(seller.SellerId);
            if (values == null)
            {
                return View(new List<ResultOrderDetailBySellerDto>());
            }
            return View(values);
        }
    }
}
