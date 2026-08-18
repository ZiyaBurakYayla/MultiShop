using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/SpecialOffer")]
    public class SpecialOfferController : Controller
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOfferController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _specialOfferService.GetAllSpecialOffersAsync();
            if (values == null)
            {
                return View(new List<ResultSpecialOfferDto>());
            }
            return View(values);
        }

        [Route("CreateSpecialOffer")]
        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            return View();
        }

        [Route("CreateSpecialOffer")]
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            if (createSpecialOfferDto == null || !ModelState.IsValid)
            {
                return View();
            }
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            var values = await _specialOfferService.GetSpecialOfferByIdAsync(id);
            if (values == null)
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View(values);
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            if (updateSpecialOfferDto == null || !ModelState.IsValid)
            {
                return View(updateSpecialOfferDto);
            }
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
    }
}
