using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IValidator<UpdateOrderDetailCommand> _validator;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository, IValidator<UpdateOrderDetailCommand> validator)
        {
            _orderDetailRepository = orderDetailRepository;
            _validator = validator;
        }
        public async Task HandleAsync(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            await _validator.ValidateAndThrowAsync(updateOrderDetailCommand);
            await _orderDetailRepository.UpdateAsync(new OrderDetail
            {
                OrderDetailId = updateOrderDetailCommand.OrderDetailId,
                ProductId = updateOrderDetailCommand.ProductId,
                ProductName = updateOrderDetailCommand.ProductName,
                ProductPrice = updateOrderDetailCommand.ProductPrice,
                ProductAmount = updateOrderDetailCommand.ProductAmount,
                ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice,
                OrderingId = updateOrderDetailCommand.OrderingId
            });
        }
    }
}
