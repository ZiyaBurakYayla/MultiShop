using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface IUserCommentService
    {
        Task<List<ResultUserCommentDto>> GetMyCommentsAsync();
        Task<UpdateUserCommentDto> GetMyCommentByIdAsync(int id);
        Task<bool> UpdateMyCommentAsync(UpdateUserCommentDto commentDto);
        Task<bool> DeleteMyCommentAsync(int id);
    }
}
