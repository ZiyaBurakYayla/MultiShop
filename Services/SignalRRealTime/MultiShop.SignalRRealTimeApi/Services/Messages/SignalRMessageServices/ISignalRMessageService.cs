namespace MultiShop.SignalRRealTimeApi.Services.Messages.SignalRMessageServices
{
    public interface ISignalRMessageService
    {
        Task<int> GetTotalMessageCountByReceiverIdAsync(string id);
        Task<int> GetTotalMessageCountAsync();

    }
}
