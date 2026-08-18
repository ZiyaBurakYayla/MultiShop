namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices
{
    public interface ICommentStatisticService
    {
        Task<int> GetActiveCommentCountAsync();
        Task<int> GetPassiveCommentCountAsync();
        Task<int> GetTotalCommentCountAsync();
    }
}
