using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
       private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IUserIdentityService _userService;

        public UserController(IUserIdentityService userService, ICargoCustomerService cargoCustomerService)
        {
            _userService = userService;
            _cargoCustomerService = cargoCustomerService;
        }


        public async Task<IActionResult> UserList()
        {
            var values = await _userService.GetAllUserInfo();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> UserAddressInfo(string id)
        {
            var value = await _cargoCustomerService.GetCargoInfoCustomerByIdAsync(id);
            return View(value);
        }
    }
}
