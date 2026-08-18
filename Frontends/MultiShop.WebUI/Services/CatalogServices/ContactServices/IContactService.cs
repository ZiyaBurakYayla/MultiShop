using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public interface IContactService
    {
        Task<GetByIdContactDto> GetContactByIdAsync(string ContactId);
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task CreateContactAsync(CreateContactDto ContactDto);
        Task UpdateContactAsync(UpdateContactDto ContactDto);
        Task DeleteContactAsync(string ContactId);
    }
}
