using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Services.IdentityServices.UserServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userService.GetUserInfo();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserDto updateUserDto)
        {
            if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword) && updateUserDto.NewPassword.Length < 6)
            {
                TempData["ProfileError"] = "Yeni şifre en az 6 karakter olmalıdır.";
                return RedirectToAction("Index");
            }

            var error = await _userService.UpdateUserAsync(updateUserDto);
            if (error != null)
            {
                TempData["ProfileError"] = error;
                return RedirectToAction("Index");
            }

            TempData["ProfileSuccess"] = "Profil bilgileriniz güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
