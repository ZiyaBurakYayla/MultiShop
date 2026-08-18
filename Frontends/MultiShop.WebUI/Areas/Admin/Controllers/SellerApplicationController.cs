using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.IdentityServices.RoleServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/SellerApplication")]
    public class SellerApplicationController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IRoleService _roleService;

        public SellerApplicationController(ISellerService sellerService, IRoleService roleService)
        {
            _sellerService = sellerService;
            _roleService = roleService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _sellerService.GetSellersByStatusAsync("Pending");
            if (values == null)
            {
                return View(new List<ResultSellerDto>());
            }
            return View(values);
        }

        [Route("Approve/{id}")]
        public async Task<IActionResult> Approve(string id)
        {
            var seller = await _sellerService.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return RedirectToAction("Index");
            }
            seller.Status = "Approved";
            await _sellerService.UpdateSellerAsync(seller);
            await _roleService.AssignRoleAsync(seller.OwnerUserId, "Seller");
            return RedirectToAction("Index");
        }

        [Route("Reject/{id}")]
        public async Task<IActionResult> Reject(string id)
        {
            var seller = await _sellerService.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return RedirectToAction("Index");
            }
            seller.Status = "Rejected";
            await _sellerService.UpdateSellerAsync(seller);
            return RedirectToAction("Index");
        }
    }
}
