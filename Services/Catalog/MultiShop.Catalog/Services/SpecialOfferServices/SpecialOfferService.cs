using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
        private readonly IMapper _mapper;
        public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName);
            _mapper = mapper;
        }
        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto SpecialOfferDto)
        {
            var value = _mapper.Map<SpecialOffer>(SpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(value);
        }

        public async Task DeleteSpecialOfferAsync(string SpecialOfferId)
        {
            await _specialOfferCollection.DeleteOneAsync(z => z.SpecialOfferId == SpecialOfferId);
        }

        public Task SpecialOfferChangeStatusToFalse(string SpecialOfferId)
        {
            throw new NotImplementedException();
        }

        public Task SpecialOfferChangeStatusToTrue(string SpecialOfferId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var values = await _specialOfferCollection.Find(SpecialOffer => true).ToListAsync();
            var result = _mapper.Map<List<ResultSpecialOfferDto>>(values);
            return result;
        }

        public async Task<GetByIdSpecialOfferDto> GetSpecialOfferByIdAsync(string SpecialOfferId)
        {
            var value = await _specialOfferCollection.Find(x => x.SpecialOfferId == SpecialOfferId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdSpecialOfferDto>(value);
            return result;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto SpecialOfferDto)
        {
            var value = _mapper.Map<SpecialOffer>(SpecialOfferDto);
            await _specialOfferCollection.ReplaceOneAsync(x => x.SpecialOfferId == SpecialOfferDto.SpecialOfferId, value);
        }
    }
}
