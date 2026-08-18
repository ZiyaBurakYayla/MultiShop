using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;
        public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            CategoryViewbagList();
            var vales = await _categoryService.GetAllCategoriesAsync();
            if (vales == null)
            {
                return View(new List<ResultCategoryDto>());
            }
            return View(vales);
        }

        [Route("CreateCategory")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            CategoryViewbagList();
            return View();
        }

        [Route("CreateCategory")]
        [HttpPost]    
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null || !ModelState.IsValid)
            {
                CategoryViewbagList();
                return View();
            }
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            CategoryViewbagList();
            var value = await _categoryService.GetCategoryByIdAsync(id);
            if (value == null)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(value);
        }
        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategory)
        {
            if (updateCategory == null || !ModelState.IsValid)
            {
                CategoryViewbagList();
                return View(updateCategory);
            }
            await _categoryService.UpdateCategoryAsync(updateCategory);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        } 
        void CategoryViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            ViewBag.v0 = "Kategori İşlemleri";
        }
    }
}
