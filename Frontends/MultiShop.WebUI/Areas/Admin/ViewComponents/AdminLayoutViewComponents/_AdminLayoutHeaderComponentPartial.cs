using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentStatisticService _commentStatisticService;

        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentStatisticService commentStatisticService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentStatisticService = commentStatisticService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();
            if (user == null)
            {
                user = new UserDetailViewModel();
            }

            ViewBag.messageCount = 0;
            if (!string.IsNullOrEmpty(user.Id))
            {
                try
                {
                    ViewBag.messageCount = await _messageService.GetTotalMessageCountByReceiverIdAsync(user.Id);
                }
                catch
                {
                    ViewBag.messageCount = 0;
                }
            }

            try
            {
                ViewBag.totalCommentCount = await _commentStatisticService.GetTotalCommentCountAsync();
            }
            catch
            {
                ViewBag.totalCommentCount = 0;
            }

            return View(user);
        }
    }
}
