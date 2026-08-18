using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Cargo.ViewComponents.CargoLayoutViewComponents
{
    public class _CargoLayoutSideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
