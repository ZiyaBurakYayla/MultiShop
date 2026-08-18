namespace MultiShop.SignalRRealTimeApi.Services.Discounts.SignalRDiscountServices
{
    public interface ISignalRDiscountService
    {
        Task<int> GetDiscountCouponCountAsync();
    }
}
