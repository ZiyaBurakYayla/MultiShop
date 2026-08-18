using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto SpecialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto SpecialOfferDto);
        Task DeleteSpecialOfferAsync(string SpecialOfferId);
        Task<GetByIdSpecialOfferDto> GetSpecialOfferByIdAsync(string SpecialOfferId);
        Task SpecialOfferChangeStatusToTrue(string SpecialOfferId);
        Task SpecialOfferChangeStatusToFalse(string SpecialOfferId);
    }
}
