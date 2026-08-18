namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRBrandServices
{
    public interface ISignalRBrandService
    {
        Task<long> GetBrandCountAsync();
        Task<string> GetLastBrandNameAsync();
    }
}
