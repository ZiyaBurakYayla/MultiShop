namespace MultiShop.SignalRRealTimeApi.Services.Orders.SignalROrderServices
{
    public interface ISignalROrderService
    {
        Task<int> GetTotalOrderCountAsync();
        Task<int> GetTotalOrderDetailCountAsync();
    }
}
