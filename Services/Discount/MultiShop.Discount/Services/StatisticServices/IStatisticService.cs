namespace MultiShop.Discount.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<int> GetDiscountCouponCountAsync();
    }
}
