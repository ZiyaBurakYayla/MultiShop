using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _commentService.GetAllCommentsAsync();
            if (values == null)
            {
                return View(new List<ResultUserCommentDto>());
            }
            return View(values);
        }

        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }

        [Route("ApproveComment/{id}")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            await _commentService.ApproveCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }

        [Route("RejectComment/{id}")]
        public async Task<IActionResult> RejectComment(int id)
        {
            await _commentService.RejectCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }

        [Route("UpdateComment/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            var values = await _commentService.GetCommentByIdAsync(id);
            return View(values);
        }

        [Route("UpdateComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateUserCommentDto updateComment)
        {
            if (updateComment == null || !ModelState.IsValid)
            {
                return View();
            }
            updateComment.Status = true;
            await _commentService.UpdateCommentAsync(updateComment);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }
    }
}
