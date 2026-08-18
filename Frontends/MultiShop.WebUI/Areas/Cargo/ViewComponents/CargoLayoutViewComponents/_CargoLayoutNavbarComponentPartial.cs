using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Cargo.ViewComponents.CargoLayoutViewComponents
{
    public class _CargoLayoutNavbarComponentPartial : ViewComponent
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly ILoginService _loginService;

        public _CargoLayoutNavbarComponentPartial(ICargoCompanyService cargoCompanyService, ILoginService loginService)
        {
            _cargoCompanyService = cargoCompanyService;
            _loginService = loginService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companies = await _cargoCompanyService.GetAllCargoCompanyAsync();
            var company = companies?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId);
            if (company == null)
            {
                company = new ResultCargoCompanyDto();
            }
            return View(company);
        }
    }
}
