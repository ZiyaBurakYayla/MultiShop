using MultiShop.Catalog.Dtos.OfferDiscountDtos;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        Task<GetByIdOfferDiscountDto> GetOfferDiscountByIdAsync(string offerDiscountId);
        Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync();
        Task CreateOfferDiscountAsync(CreateOfferDiscountDto offerDiscountDto);
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto offerDiscountDto);
        Task DeleteOfferDiscountAsync(string offerDiscountId);
    }
}
