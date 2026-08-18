using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class MyCommentController : Controller
    {
        private readonly IUserCommentService _userCommentService;
        private readonly IProductService _productService;

        public MyCommentController(IUserCommentService userCommentService, IProductService productService)
        {
            _userCommentService = userCommentService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userCommentService.GetMyCommentsAsync();
            ViewBag.Products = await _productService.GetAllProductsAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateComment(int id)
        {
            var value = await _userCommentService.GetMyCommentByIdAsync(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Products = await _productService.GetAllProductsAsync();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateUserCommentDto updateUserCommentDto)
        {
            if (string.IsNullOrWhiteSpace(updateUserCommentDto.CommentDetail))
            {
                ViewBag.Error = "Yorum boş olamaz.";
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(updateUserCommentDto);
            }
            var result = await _userCommentService.UpdateMyCommentAsync(updateUserCommentDto);
            if (!result)
            {
                ViewBag.Error = "Yorum güncellenemedi.";
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(updateUserCommentDto);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            await _userCommentService.DeleteMyCommentAsync(id);
            return RedirectToAction("Index");
        }

    }
}
