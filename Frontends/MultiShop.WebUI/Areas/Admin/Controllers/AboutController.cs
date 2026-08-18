using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _aboutService.GetAllAboutsAsync();
            if (values == null)
            {
                return View(new List<ResultAboutDto>());
            }
            return View(values);
        }

        [Route("CreateAbout")]
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [Route("CreateAbout")]
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            if (createAboutDto == null || !ModelState.IsValid)
            {
                return View();
            }
            await _aboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            await _aboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Route("UpdateAbout/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            var values = await _aboutService.GetAboutByIdAsync(id);
            if (values == null)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View(values);
        }

        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            if (updateAboutDto == null || !ModelState.IsValid)
            {
                return View(updateAboutDto);
            }
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
    }
}
