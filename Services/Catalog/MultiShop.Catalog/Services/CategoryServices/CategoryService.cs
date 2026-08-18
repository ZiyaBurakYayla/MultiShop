using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            await _categoryCollection.DeleteOneAsync(z => z.CategoryId == categoryId);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var values = await _categoryCollection.Find(category => true).ToListAsync();
            var result = _mapper.Map<List<ResultCategoryDto>>(values);
            return result;
        }

        public async Task<GetByIdCategoryDto> GetCategoryByIdAsync(string categoryId)
        {
            var value = await _categoryCollection.Find(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdCategoryDto>(value);
            return result;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.ReplaceOneAsync(x => x.CategoryId == categoryDto.CategoryId, value);
        }
    }
}
