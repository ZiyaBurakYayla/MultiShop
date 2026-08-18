using Dapper;
using MultiShop.Discount.Context;

namespace MultiShop.Discount.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly DapperContext _context;

        public StatisticService(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> GetDiscountCouponCountAsync()
        {
            var query = "Select count (*) from Coupons";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<int>(query);
                return result.FirstOrDefault();
            }
        }
    }
}
