using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IOrderingRepository
    {
        public Task<List<Ordering>> GetOrderingByUserId(string id);
    }
}
