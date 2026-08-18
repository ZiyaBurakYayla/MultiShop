using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBasketService _basketService;

        public _NavbarUILayoutComponentPartial(ICategoryService categoryService, IBasketService basketService)
        {
            _categoryService = categoryService;
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketItemCount = 0;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                try
                {
                    var basket = await _basketService.GetBasket();
                    if (basket != null && basket.BasketItems != null)
                    {
                        ViewBag.BasketItemCount = basket.BasketItems.Count;
                    }
                }
                catch
                {
                    ViewBag.BasketItemCount = 0;
                }
            }

            var values = await _categoryService.GetAllCategoriesAsync();
            if (values == null)
            {
                return View(new List<ResultCategoryDto>());
            }
            return View(values);
        }
    }
}
