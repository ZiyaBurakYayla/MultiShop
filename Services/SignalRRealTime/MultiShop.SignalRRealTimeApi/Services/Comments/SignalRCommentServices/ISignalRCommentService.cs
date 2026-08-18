namespace MultiShop.SignalRRealTimeApi.Services.Comments.SignalRCommentServices
{
    public interface ISignalRCommentService
    {
        Task<int> GetCommentCountAsync();
        Task<int> GetActiveCommentCountAsync();
        Task<int> GetPassiveCommentCountAsync();
    }
}
