using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListCategoryFilterComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _ProductListCategoryFilterComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            if (values == null)
            {
                values = new List<ResultCategoryDto>();
            }
            ViewBag.ActiveCategoryId = id;
            return View(values);
        }
    }
}
