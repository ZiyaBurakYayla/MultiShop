using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoDetailDtos;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.CargoServices.CargoOperationServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Cargo.Controllers
{
    [Area("Cargo")]
    [Authorize(Roles = "Cargo")]
    public class ShipmentController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ICargoOperationService _cargoOperationService;
        private readonly ILoginService _loginService;

        public ShipmentController(ICargoCompanyService cargoCompanyService, ICargoDetailService cargoDetailService, ICargoOperationService cargoOperationService, ILoginService loginService)
        {
            _cargoCompanyService = cargoCompanyService;
            _cargoDetailService = cargoDetailService;
            _cargoOperationService = cargoOperationService;
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
            var values = await _cargoDetailService.GetByCompanyIdAsync(company.CargoCompanyId);
            return View(values);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var companies = await _cargoCompanyService.GetAllCargoCompanyAsync();
            var company = companies?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId);
            if (company == null)
            {
                return RedirectToAction("Index", "Default", new { area = "" });
            }
            var cargo = await _cargoDetailService.GetByBarcodeAsync(id);
            if (cargo == null || cargo.CargoCompanyId != company.CargoCompanyId)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Operations = await _cargoOperationService.GetByBarcodeAsync(id);
            return View(cargo);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string barcode, string status)
        {
            var companies = await _cargoCompanyService.GetAllCargoCompanyAsync();
            var company = companies?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId);
            if (company == null)
            {
                return RedirectToAction("Index", "Default", new { area = "" });
            }
            var cargo = await _cargoDetailService.GetByBarcodeAsync(barcode);
            if (cargo == null || cargo.CargoCompanyId != company.CargoCompanyId)
            {
                return RedirectToAction("Index");
            }
            await _cargoDetailService.UpdateStatusAsync(new UpdateCargoStatusDto
            {
                Barcode = barcode,
                Status = status
            });
            return RedirectToAction("Detail", new { id = barcode });
        }
    }
}
