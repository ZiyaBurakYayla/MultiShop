using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public async Task<IActionResult> InBox()
        {
            var user = await _userService.GetUserInfo();
            var values = await _messageService.GetInboxMessageAsync(user.Id);
            return View(values);
        }

        public async Task<IActionResult> SendBox()
        {
            var user = await _userService.GetUserInfo();
            ViewBag.Name = user.Name;
            ViewBag.Surname = user.Surname;
            var values = await _messageService.GetSendboxMessageAsync(user.Id);
            return View(values);
        }
    }
}
