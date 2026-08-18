using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;

namespace MultiShop.WebUI.Areas.Seller.ViewComponents.SellerLayoutViewComponents
{
    public class _SellerLayoutNavbarComponentPartial : ViewComponent
    {
        private readonly ISellerService _sellerService;
        private readonly ILoginService _loginService;

        public _SellerLayoutNavbarComponentPartial(ISellerService sellerService, ILoginService loginService)
        {
            _sellerService = sellerService;
            _loginService = loginService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            var seller = sellers?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId && x.Status == "Approved");
            if (seller == null)
            {
                seller = new ResultSellerDto();
            }
            return View(seller);
        }
    }
}
