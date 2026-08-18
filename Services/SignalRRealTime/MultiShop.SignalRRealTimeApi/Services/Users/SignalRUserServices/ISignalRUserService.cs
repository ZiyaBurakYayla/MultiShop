namespace MultiShop.SignalRRealTimeApi.Services.Users.SignalRUserServices
{
    public interface ISignalRUserService
    {
        Task<int> GetUserCountAsync();
    }
}
