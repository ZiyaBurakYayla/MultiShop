using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Seller.ViewComponents.SellerLayoutViewComponents
{
    public class _SellerLayoutSideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
