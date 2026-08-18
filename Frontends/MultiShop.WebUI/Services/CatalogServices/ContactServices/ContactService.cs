using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactDto contactDto)
        {
            await _httpClient.PostAsJsonAsync<CreateContactDto>("contacts", contactDto);
        }

        public async Task DeleteContactAsync(string ContactId)
        {
            await _httpClient.DeleteAsync("contacts?id=" + ContactId);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var response = await _httpClient.GetAsync("contacts");
            var values = await response.Content.ReadFromJsonAsync<List<ResultContactDto>>();
            return values;
        }

        public async Task<GetByIdContactDto> GetContactByIdAsync(string ContactId)
        {
            var response = await _httpClient.GetAsync("contacts/" + ContactId);
            var values = await response.Content.ReadFromJsonAsync<GetByIdContactDto>();
            return values;
        }

        public async Task UpdateContactAsync(UpdateContactDto ContactDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateContactDto>("contacts", ContactDto);
        }
    }
}
