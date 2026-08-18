using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.CargoServices.CargoOperationServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class CargoController : Controller
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ICargoOperationService _cargoOperationService;
        private readonly ILoginService _loginService;

        public CargoController(ICargoDetailService cargoDetailService, ICargoOperationService cargoOperationService, ILoginService loginService)
        {
            _cargoDetailService = cargoDetailService;
            _cargoOperationService = cargoOperationService;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _cargoDetailService.GetMyCargosAsync();
            return View(values);
        }

        public async Task<IActionResult> Track(string id)
        {
            var cargo = await _cargoDetailService.GetByBarcodeAsync(id);
            if (cargo == null || cargo.UserId != _loginService.GetUserId)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Operations = await _cargoOperationService.GetByBarcodeAsync(id);
            return View(cargo);
        }
    }
}
