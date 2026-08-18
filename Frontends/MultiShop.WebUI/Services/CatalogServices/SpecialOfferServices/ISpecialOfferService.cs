using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<UpdateSpecialOfferDto> GetSpecialOfferByIdAsync(string id);
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOffersAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto specialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto specialOfferDto);
        Task DeleteSpecialOfferAsync(string id);
    }
}
