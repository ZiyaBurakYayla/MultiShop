namespace MultiShop.Order.WebApi.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<int> GetTotalOrderCountAsync();
        Task<int> GetTotalOrderDetailCountAsync();
    }
}
