using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<GetDiscountCodeDetailByCodeDto> GetDiscountCode(string code);
        Task<int> GetDiscountCouponRate(string code);
    }
}
