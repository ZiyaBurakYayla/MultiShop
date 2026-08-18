using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SellerDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SellerServices
{
    public class SellerService : ISellerService
    {
        private readonly IMongoCollection<Seller> _sellerCollection;
        private readonly IMapper _mapper;
        public SellerService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _sellerCollection = database.GetCollection<Seller>(_databaseSettings.SellerCollectionName);
            _mapper = mapper;
        }

        public async Task CreateSellerAsync(CreateSellerDto sellerDto)
        {
            var value = _mapper.Map<Seller>(sellerDto);
            value.Status = "Pending";
            await _sellerCollection.InsertOneAsync(value);
        }

        public async Task DeleteSellerAsync(string sellerId)
        {
            await _sellerCollection.DeleteOneAsync(z => z.SellerId == sellerId);
        }

        public async Task<List<ResultSellerDto>> GetAllSellerAsync()
        {
            var values = await _sellerCollection.Find(seller => true).ToListAsync();
            var result = _mapper.Map<List<ResultSellerDto>>(values);
            return result;
        }

        public async Task<List<ResultSellerDto>> GetSellersByStatusAsync(string status)
        {
            var values = await _sellerCollection.Find(x => x.Status == status).ToListAsync();
            var result = _mapper.Map<List<ResultSellerDto>>(values);
            return result;
        }

        public async Task<GetByIdSellerDto> GetSellerByIdAsync(string sellerId)
        {
            var value = await _sellerCollection.Find(x => x.SellerId == sellerId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdSellerDto>(value);
            return result;
        }

        public async Task UpdateSellerAsync(UpdateSellerDto sellerDto)
        {
            var value = _mapper.Map<Seller>(sellerDto);
            await _sellerCollection.ReplaceOneAsync(x => x.SellerId == sellerDto.SellerId, value);
        }
    }
}
