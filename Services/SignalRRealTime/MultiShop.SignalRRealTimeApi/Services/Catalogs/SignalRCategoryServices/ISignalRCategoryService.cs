namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRCategoryServices
{
    public interface ISignalRCategoryService
    {
        Task<long> GetCategoryCountAsync();
    }
}
