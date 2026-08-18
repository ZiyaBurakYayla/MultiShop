namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public interface ICatalogStatisticService
    {
        Task<long> GetCategoryCountAsync();
        Task<long> GetProductCountAsync();
        Task<decimal> GetProductAvgPriceAsync();
        Task<long> GetBrandCountAsync();
        Task<string> GetMaxPriceProductNameAsync();
        Task<string> GetMinPriceProductNameAsync();
        Task<long> GetProductImageCountAsync();
        Task<long> GetSpecialOfferCountAsync();
        Task<long> GetFeatureCountAsync();
        Task<string> GetLastBrandNameAsync();
        Task<string> GetLastProductNameAsync();
    }
}
