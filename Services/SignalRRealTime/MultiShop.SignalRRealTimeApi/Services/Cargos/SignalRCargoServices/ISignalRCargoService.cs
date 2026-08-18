namespace MultiShop.SignalRRealTimeApi.Services.Cargos.SignalRCargoServices
{
    public interface ISignalRCargoService
    {
        Task<int> GetTotalCargoCustomerCountAsync();
    }
}
