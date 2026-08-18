using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Persistence.Context;

namespace MultiShop.Order.WebApi.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly OrderContext _orderContext;

        public StatisticService(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<int> GetTotalOrderCountAsync()
        {
            return await _orderContext.Orderings.CountAsync();
        }

        public async Task<int> GetTotalOrderDetailCountAsync()
        {
            return await _orderContext.OrderDetails.CountAsync();
        }
    }
}
