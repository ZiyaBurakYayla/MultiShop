using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutNavbarComponentPartial : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IBasketService _basketService;

        public _UserLayoutNavbarComponentPartial(IUserService userService, IMessageService messageService, IBasketService basketService)
        {
            _userService = userService;
            _messageService = messageService;
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();
            if (user == null)
            {
                user = new UserDetailViewModel();
            }

            ViewBag.MessageCount = 0;
            if (!string.IsNullOrEmpty(user.Id))
            {
                try
                {
                    ViewBag.MessageCount = await _messageService.GetTotalMessageCountByReceiverIdAsync(user.Id);
                }
                catch
                {
                    ViewBag.MessageCount = 0;
                }
            }

            ViewBag.BasketItemCount = 0;
            try
            {
                var basket = await _basketService.GetBasket();
                if (basket != null && basket.BasketItems != null)
                {
                    ViewBag.BasketItemCount = basket.BasketItems.Count;
                }
            }
            catch
            {
                ViewBag.BasketItemCount = 0;
            }

            return View(user);
        }
    }
}
