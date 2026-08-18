using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly IMongoCollection<OfferDiscount> _offerDiscountCollection;
        private readonly IMapper _mapper;
        public OfferDiscountService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _offerDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName);
            _mapper = mapper;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto offerDiscountDto)
        {
            var value = _mapper.Map<OfferDiscount>(offerDiscountDto);
            await _offerDiscountCollection.InsertOneAsync(value);
        }

        public async Task DeleteOfferDiscountAsync(string offerDiscountId)
        {
            await _offerDiscountCollection.DeleteOneAsync(z => z.OfferDiscountId == offerDiscountId);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
        {
            var values = await _offerDiscountCollection.Find(offerDiscount => true).ToListAsync();
            var result = _mapper.Map<List<ResultOfferDiscountDto>>(values);
            return result;
        }

        public async Task<GetByIdOfferDiscountDto> GetOfferDiscountByIdAsync(string offerDiscountId)
        {
            var value = await _offerDiscountCollection.Find(x => x.OfferDiscountId == offerDiscountId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdOfferDiscountDto>(value);
            return result;
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto offerDiscountDto)
        {
            var value = _mapper.Map<OfferDiscount>(offerDiscountDto);
            await _offerDiscountCollection.ReplaceOneAsync(x => x.OfferDiscountId == offerDiscountDto.OfferDiscountId, value);
        }
    }
}
