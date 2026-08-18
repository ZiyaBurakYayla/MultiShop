using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _service;

        public BasketService(RedisService service)
        {
            _service = service;
        }

        public async Task DeleteBasket(string userId)
        {
            await _service.GetDb().KeyDeleteAsync(userId);
        }

        public async Task<BasketTotalDto> GetBasket(string userId)
        {
            var existBasket = await _service.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
            }
            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }

        public async Task SaveBasket(BasketTotalDto basket)
        {
            await _service.GetDb().StringSetAsync(basket.UserId,JsonSerializer.Serialize(basket));
        }
    }
}
