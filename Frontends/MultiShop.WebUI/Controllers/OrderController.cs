using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.AddressDtos;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.OrderServices.AddressServices;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;

        public OrderController(IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Siparişler";
            ViewBag.directory3 = "Sipariş İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateAddressDto createAddressDto)
        {
            var values = await _userService.GetUserInfo();
            createAddressDto.UserId = values.Id;

            if (createAddressDto.Description == null)
            {
                createAddressDto.Description = "açıklama yapılmadı";
            }
            await _addressService.CreateAddressAsync(createAddressDto);

            return RedirectToAction("Index","Payment");
        }
    }
}
