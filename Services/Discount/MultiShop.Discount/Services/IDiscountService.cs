using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultCouponDto>> GetAllCouponAsync();
        Task<GetByIdCouponDto> GetByIdCouponAsync(int couponId);
        Task CreateCouponAsync(CreateCouponDto createCouponDto);
        Task UpdateCouponAsync(UpdateCouponDto updateCouponDto);
        Task DeleteCouponAsync(int couponId);
        Task<ResultCouponDto> GetCodeDetailByCodeAsync(string code);
        int GetDiscountCouponRate(string code); 
    }
}
