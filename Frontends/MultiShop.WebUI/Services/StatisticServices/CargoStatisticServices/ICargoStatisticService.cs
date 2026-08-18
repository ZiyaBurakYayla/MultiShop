namespace MultiShop.WebUI.Services.StatisticServices.CargoStatisticServices
{
    public interface ICargoStatisticService
    {
        Task<int> GetTotalCargoCustomerCountAsync();
    }
}
