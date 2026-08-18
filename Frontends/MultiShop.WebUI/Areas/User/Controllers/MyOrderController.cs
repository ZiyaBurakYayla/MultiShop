using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;
using MultiShop.WebUI.Services.OrderServices.OrderingServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class MyOrderController : Controller
    {
        private readonly IOrderingService _orderingService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IUserService _userService;

        public MyOrderController(IOrderingService orderingService, IOrderDetailService orderDetailService, IUserService userService)
        {
            _orderingService = orderingService;
            _orderDetailService = orderDetailService;
            _userService = userService;
        }

        public async Task<IActionResult> MyOrderList()
        {
            var user = await _userService.GetUserInfo();
            var values = await _orderingService.GetOrderingByUserId(user.Id);
            return View(values);
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            var user = await _userService.GetUserInfo();
            var orders = await _orderingService.GetOrderingByUserId(user.Id);
            var order = orders?.FirstOrDefault(x => x.OrderingId == id);
            if (order == null)
            {
                return RedirectToAction("MyOrderList");
            }

            ViewBag.Order = order;
            var details = await _orderDetailService.GetOrderDetailsByOrderingIdAsync(id);
            return View(details);
        }
    }
}
