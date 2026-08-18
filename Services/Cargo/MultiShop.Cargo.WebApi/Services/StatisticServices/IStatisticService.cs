namespace MultiShop.Cargo.WebApi.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<int> GetTotalCargoCustomerCountAsync();
    }
}
