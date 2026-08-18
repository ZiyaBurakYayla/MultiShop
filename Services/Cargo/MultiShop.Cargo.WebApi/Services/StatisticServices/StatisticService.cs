using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly CargoContext _cargoContext;

        public StatisticService(CargoContext cargoContext)
        {
            _cargoContext = cargoContext;
        }

        public async Task<int> GetTotalCargoCustomerCountAsync()
        {
            return await _cargoContext.CargoCustomers.CountAsync();
        }
    }
}
