using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Persistence.Context;

namespace MultiShop.Order.Persistence.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderContext _context;

        public OrderDetailRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsBySellerId(string sellerId)
        {
            var values = await _context.OrderDetails
                .Include(x => x.Ordering)
                .Where(x => x.SellerId == sellerId)
                .OrderByDescending(x => x.OrderDetailId)
                .ToListAsync();
            return values;
        }
    }
}
