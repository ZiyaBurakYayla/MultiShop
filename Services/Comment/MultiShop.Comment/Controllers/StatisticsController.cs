using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly CommentContext _commentContext;

        public StatisticsController(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        [HttpGet("GetActiveCommentCount")]
        public async Task<IActionResult> GetActiveCommentCountAsync()
        {
            int value = await _commentContext.UserComments.Where(z => z.Status == true).CountAsync();
            return Ok(value);
        }

        [HttpGet("GetPassiveCommentCount")]
        public async Task<IActionResult> GetPassiveCommentCountAsync()
        {
            int value = await _commentContext.UserComments.Where(z => z.Status == false).CountAsync();
            return Ok(value);
        }

        [HttpGet("GetTotalCommentCount")]
        public async Task<IActionResult> GetTotalCommentCountAsync()
        {
            int value = await _commentContext.UserComments.CountAsync();
            return Ok(value);
        }
    }
}
