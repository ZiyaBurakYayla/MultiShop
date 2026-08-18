using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.IdentityServices.RoleServices;
using MultiShop.WebUI.Services.IdentityServices.UserServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CargoController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public CargoController(ICargoCompanyService cargoCompanyService, ICargoDetailService cargoDetailService, IUserService userService, IRoleService roleService)
        {
            _cargoCompanyService = cargoCompanyService;
            _cargoDetailService = cargoDetailService;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await _cargoCompanyService.GetAllCargoCompanyAsync();
            if (values == null)
            {
                return View(new List<ResultCargoCompanyDto>());
            }
            ViewBag.Users = await _userService.GetAllUserListAsync();
            return View(values);
        }

        public async Task<IActionResult> CargoDetailList()
        {
            var values = await _cargoDetailService.GetAllCargoDetailsAsync();
            if (values == null)
            {
                return View(new List<MultiShop.DtoLayer.CargoDtos.CargoDetailDtos.ResultCargoDetailDto>());
            }
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCargoCompany()
        {
            ViewBag.Users = await _userService.GetAllUserListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            if (createCargoCompanyDto == null || !ModelState.IsValid)
            {
                ViewBag.Users = await _userService.GetAllUserListAsync();
                return View();
            }
            await _cargoCompanyService.CreateCargoCompanyAsync(createCargoCompanyDto);
            if (!string.IsNullOrEmpty(createCargoCompanyDto.OwnerUserId))
            {
                await _roleService.AssignRoleAsync(createCargoCompanyDto.OwnerUserId, "Cargo");
            }
            return RedirectToAction("CargoCompanyList", "Cargo", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteCargoCompany(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("CargoCompanyList", "Cargo", new { area = "Admin" });
            }
            await _cargoCompanyService.DeleteCargoCompanyAsync(id);
            return RedirectToAction("CargoCompanyList", "Cargo", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCargoCompany(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("CargoCompanyList", "Cargo", new { area = "Admin" });
            }
            ViewBag.Users = await _userService.GetAllUserListAsync();
            var values = await _cargoCompanyService.GetByIdCargoCompanyAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            if (updateCargoCompanyDto == null || !ModelState.IsValid)
            {
                ViewBag.Users = await _userService.GetAllUserListAsync();
                return View();
            }
            await _cargoCompanyService.UpdateCargoCompanyAsync(updateCargoCompanyDto);
            if (!string.IsNullOrEmpty(updateCargoCompanyDto.OwnerUserId))
            {
                await _roleService.AssignRoleAsync(updateCargoCompanyDto.OwnerUserId, "Cargo");
            }
            return RedirectToAction("CargoCompanyList", "Cargo", new { area = "Admin" });
        }
    }
}
