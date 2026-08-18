namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRFeatureServices
{
    public interface ISignalRFeatureService
    {
        Task<long> GetFeatureCountAsync();
    }
}
