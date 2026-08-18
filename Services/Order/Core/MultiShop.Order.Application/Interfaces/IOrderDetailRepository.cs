using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetail>> GetOrderDetailsBySellerId(string sellerId);
    }
}
