using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _ContactCollection;
        private readonly IMapper _mapper;
        public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ContactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto ContactDto)
        {
            var value = _mapper.Map<Contact>(ContactDto);
            await _ContactCollection.InsertOneAsync(value);
        }

        public async Task DeleteContactAsync(string ContactId)
        {
            await _ContactCollection.DeleteOneAsync(z => z.ContactId == ContactId);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var values = await _ContactCollection.Find(Contact => true).ToListAsync();
            var result = _mapper.Map<List<ResultContactDto>>(values);
            return result;
        }

        public async Task<GetByIdContactDto> GetContactByIdAsync(string ContactId)
        {
            var value = await _ContactCollection.Find(x => x.ContactId == ContactId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdContactDto>(value);
            return result;
        }

        public async Task UpdateContactAsync(UpdateContactDto ContactDto)
        {
            var value = _mapper.Map<Contact>(ContactDto);
            await _ContactCollection.ReplaceOneAsync(x => x.ContactId == ContactDto.ContactId, value);
        }
    }
}
