using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Seller.ViewComponents.SellerLayoutViewComponents
{
    public class _SellerLayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
