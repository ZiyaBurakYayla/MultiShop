using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Cargo.ViewComponents.CargoLayoutViewComponents
{
    public class _CargoLayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
