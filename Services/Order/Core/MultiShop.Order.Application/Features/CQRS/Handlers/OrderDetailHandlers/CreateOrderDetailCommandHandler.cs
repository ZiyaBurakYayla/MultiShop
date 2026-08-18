using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.RabbitMQ;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IValidator<CreateOrderDetailCommand> _validator;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository, IValidator<CreateOrderDetailCommand> validator, RabbitMQPublisher rabbitMQPublisher)
        {
            _orderDetailRepository = orderDetailRepository;
            _validator = validator;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public async Task HandleAsync(CreateOrderDetailCommand createOrderDetailCommand)
        {
            await _validator.ValidateAndThrowAsync(createOrderDetailCommand);
            await _orderDetailRepository.CreateAsync(new OrderDetail
            {
                ProductId = createOrderDetailCommand.ProductId,
                ProductName = createOrderDetailCommand.ProductName,
                ProductPrice = createOrderDetailCommand.ProductPrice,
                ProductAmount = createOrderDetailCommand.ProductAmount,
                ProductTotalPrice = createOrderDetailCommand.ProductTotalPrice,
                OrderingId = createOrderDetailCommand.OrderingId,
                Sku = createOrderDetailCommand.Sku,
                SellerId = createOrderDetailCommand.SellerId
            });

            _rabbitMQPublisher.Publish(new ProductStockReduceEvent
            {
                Sku = createOrderDetailCommand.Sku,
                Quantity = createOrderDetailCommand.ProductAmount
            });
        }
    }
}
