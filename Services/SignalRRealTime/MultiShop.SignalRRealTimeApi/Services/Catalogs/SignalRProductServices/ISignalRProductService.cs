namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductServices
{
    public interface ISignalRProductService
    {
        Task<long> GetProductCountAsync();
        Task<decimal> GetProductAvgPriceAsync();
        Task<string> GetMaxPriceProductNameAsync();
        Task<string> GetMinPriceProductNameAsync();
        Task<string> GetLastProductNameAsync();
    }
}
