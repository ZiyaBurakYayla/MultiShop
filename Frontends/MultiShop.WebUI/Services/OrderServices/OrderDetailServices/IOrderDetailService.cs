using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task<string> CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto);
        Task<List<ResultOrderDetailBySellerDto>> GetOrderDetailsBySellerIdAsync(string sellerId);
        Task<List<ResultOrderDetailDto>> GetOrderDetailsByOrderingIdAsync(int orderingId);
    }
}
