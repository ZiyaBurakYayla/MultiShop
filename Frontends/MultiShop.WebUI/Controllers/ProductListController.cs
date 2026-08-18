using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.IdentityServices.UserServices;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public ProductListController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }
        public IActionResult Index(string id)
        {
            ViewBag.directory1 = "MultiShop";
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.directory3 = "Tüm Ürünler";
            }
            else
            {
                ViewBag.directory2 = "Ürünler";
                ViewBag.directory3 = "Ürün Listesi";
            }
            ViewBag.i = id;
            return View();
        }
        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Detayları";
            ViewBag.x = id;
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> AddComment(string id)
        {
            var value = await _commentService.CommentsListByProductId(id);
            if (value == null)
            {
                return PartialView();
            }
            return PartialView(value);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateUserCommentDto userCommentDto)
        {
            var user = await _userService.GetUserInfo();
            userCommentDto.UserId = user.Id;
            userCommentDto.NameSurname = user.Name + " " + user.Surname;
            userCommentDto.Email = user.Email;
            userCommentDto.CreatedDate = DateTime.Now;
            userCommentDto.Status = false;
            userCommentDto.ImageUrl = string.IsNullOrWhiteSpace(user.ImageUrl)
                ? "https://ui-avatars.com/api/?name=" + Uri.EscapeDataString(userCommentDto.NameSurname) + "&background=random"
                : user.ImageUrl;
            await _commentService.CreateCommentAsync(userCommentDto);
            return Redirect("/ProductList/ProductDetail/" + userCommentDto.ProductId);
        }
    }
}
