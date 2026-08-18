using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Cargo.Controllers
{
    [Area("Cargo")]
    [Authorize(Roles = "Cargo")]
    public class DashboardController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ILoginService _loginService;

        public DashboardController(ICargoCompanyService cargoCompanyService, ICargoDetailService cargoDetailService, ILoginService loginService)
        {
            _cargoCompanyService = cargoCompanyService;
            _cargoDetailService = cargoDetailService;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _cargoCompanyService.GetAllCargoCompanyAsync();
            var company = companies?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId);
            if (company == null)
            {
                return RedirectToAction("Index", "Default", new { area = "" });
            }
            var cargos = await _cargoDetailService.GetByCompanyIdAsync(company.CargoCompanyId);
            ViewBag.CompanyName = company.CargoCompanyName;
            ViewBag.Total = cargos.Count;
            ViewBag.Preparing = cargos.Count(x => x.Status == "Hazırlanıyor");
            ViewBag.OnTheWay = cargos.Count(x => x.Status == "Yolda");
            ViewBag.Delivered = cargos.Count(x => x.Status == "Teslim Edildi");
            return View(cargos);
        }
    }
}
