using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class RemoveOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task HandleAsync(RemoveOrderDetailCommand removeOrderDetailCommand)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(removeOrderDetailCommand.OrderDetailId);
            await _orderDetailRepository.DeleteAsync(orderDetail);
        }
    }
}
