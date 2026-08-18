using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<UpdateUserCommentDto> GetCommentByIdAsync(string id);
        Task<List<ResultUserCommentDto>> GetAllCommentsAsync();
        Task<List<ResultUserCommentDto>> CommentsListByProductId(string id);
        Task CreateCommentAsync(CreateUserCommentDto CommentDto);
        Task UpdateCommentAsync(UpdateUserCommentDto CommentDto);
        Task DeleteCommentAsync(string id);
        Task ApproveCommentAsync(int id);
        Task RejectCommentAsync(int id);
    }
}
