namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices
{
    public interface IMessageStatisticService
    {
        Task<int> GetTotalMessageCountAsync();
        Task<int> GetTotalMessageCountByReceiverIdAsync(string id);
    }
}
