using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();
            if (!values.BasketItems.Any(x => x.ProductId == basketItemDto.ProductId))
            {
                values.BasketItems.Add(basketItemDto);
            }
            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync("baskets");
        }

        public async Task<bool> DiscardBasketItem(string ProductId)
        {
            var values = await GetBasket();
            if (values != null)
            {
                var discartedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == ProductId);
                var result = values.BasketItems.Remove(discartedItem);
                await SaveBasket(values);
                return true;
            }
            return false;
        }

        public async Task<BasketTotalDto> UpdateBasketItemQuantity(string productId, int quantity)
        {
            var values = await GetBasket();
            if (values == null || values.BasketItems == null)
            {
                return values;
            }
            var item = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                return values;
            }
            if (quantity < 1)
            {
                values.BasketItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            await SaveBasket(values);
            return values;
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var values = await _httpClient.GetFromJsonAsync<BasketTotalDto>("baskets");
            if (values == null)
            {
                return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
            }
            return values;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basketTotalDto);
        }
    }
}
