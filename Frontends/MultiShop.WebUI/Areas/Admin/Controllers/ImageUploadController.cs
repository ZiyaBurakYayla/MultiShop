using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.ImageUploadServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ImageUpload")]
    public class ImageUploadController : Controller
    {
        private readonly IImageUploadService _imageUploadService;

        public ImageUploadController(IImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.error = "Lütfen bir dosya seçin.";
                return View();
            }

            var url = await _imageUploadService.UploadImageAsync(file);
            if (url == null)
            {
                ViewBag.error = "Yükleme başarısız. Images servisi çalışıyor mu?";
                return View();
            }

            ViewBag.uploadedUrl = url;
            return View();
        }
    }
}
