using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using MultiShop.WebUI.Services.IdentityServices.RegisterServices;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
            if (createRegisterDto.Password != createRegisterDto.ConfirmPassword)
            {
                ViewBag.RegisterError = "Şifreler eşleşmiyor.";
                return View();
            }

            var error = await _registerService.CreateUserAsync(createRegisterDto);
            if (error == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.RegisterError = error;
            return View();
        }
    }
}
