using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;
using System.Linq;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class SellerApplicationController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;

        public SellerApplicationController(ISellerService sellerService, ILoginService loginService)
        {
            _sellerService = sellerService;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            var existing = sellers?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId);
            return View(existing);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(string storeName, string description, string logoUrl)
        {
            var userId = _loginService.GetUserId;
            var sellers = await _sellerService.GetAllSellersAsync();
            var existing = sellers?.FirstOrDefault(x => x.OwnerUserId == userId);

            if (existing != null && existing.Status == "Rejected")
            {
                var updateSellerDto = new UpdateSellerDto
                {
                    SellerId = existing.SellerId,
                    StoreName = storeName,
                    Description = description,
                    LogoUrl = logoUrl,
                    OwnerUserId = userId,
                    Status = "Pending"
                };
                await _sellerService.UpdateSellerAsync(updateSellerDto);
            }
            else if (existing == null)
            {
                var createSellerDto = new CreateSellerDto
                {
                    StoreName = storeName,
                    Description = description,
                    LogoUrl = logoUrl,
                    OwnerUserId = userId
                };
                await _sellerService.CreateSellerAsync(createSellerDto);
            }

            TempData["SuccessMessage"] = "Satıcı başvurunuz alındı, onay bekleniyor.";
            return RedirectToAction("Index");
        }
    }
}
