using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;
using MultiShop.Comment.LoginServices;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _commentContext;
        private readonly ILoginService _loginService;

        public CommentsController(CommentContext commentContext, ILoginService loginService)
        {
            _commentContext = commentContext;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _commentContext.UserComments.ToListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdComment(int id)
        {
            var value = await _commentContext.UserComments.FindAsync(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment userComment)
        {
            _commentContext.UserComments.Add(userComment);
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UserComment userComment)
        {
            var current = await _commentContext.UserComments.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserCommentId == userComment.UserCommentId);
            if (current == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            userComment.UserId = current.UserId;
            userComment.Status = false;
            _commentContext.UserComments.Update(userComment);
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var value = await _commentContext.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            _commentContext.Remove(value);
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }

        [HttpGet("CommentsListByProductId/{id}")]
        public async Task<IActionResult> CommentsListByProductId(string id)
        {
            var values = await _commentContext.UserComments.Where(x => x.ProductId == id).Where(x => x.Status == true).ToListAsync();
            return Ok(values);
        }

        [HttpGet("MyComments")]
        public async Task<IActionResult> MyComments()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            var values = await _commentContext.UserComments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
            return Ok(values);
        }

        [HttpGet("MyComments/{id}")]
        public async Task<IActionResult> GetMyComment(int id)
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            var value = await _commentContext.UserComments.FirstOrDefaultAsync(x => x.UserCommentId == id && x.UserId == userId);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            return Ok(value);
        }

        [HttpPut("UpdateMyComment")]
        public async Task<IActionResult> UpdateMyComment(UserComment userComment)
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            var current = await _commentContext.UserComments.FirstOrDefaultAsync(x => x.UserCommentId == userComment.UserCommentId);
            if (current == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            current.CommentDetail = userComment.CommentDetail;
            current.Rating = userComment.Rating;
            current.Status = false;
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }

        [HttpDelete("DeleteMyComment/{id}")]
        public async Task<IActionResult> DeleteMyComment(int id)
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            var value = await _commentContext.UserComments.FirstOrDefaultAsync(x => x.UserCommentId == id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            _commentContext.Remove(value);
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }

        [HttpGet("ApproveComment/{id}")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var value = await _commentContext.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            value.Status = true;
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }

        [HttpGet("RejectComment/{id}")]
        public async Task<IActionResult> RejectComment(int id)
        {
            var value = await _commentContext.UserComments.FindAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            value.Status = false;
            await _commentContext.SaveChangesAsync();
            return Ok("Başarılı");
        }
    }
}
