using MultiShop.DtoLayer.OrderDtos.OrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public interface IOrderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId);
        Task CreateOrdering(CreateOrderingDto createOrderingDto);
    }
}
