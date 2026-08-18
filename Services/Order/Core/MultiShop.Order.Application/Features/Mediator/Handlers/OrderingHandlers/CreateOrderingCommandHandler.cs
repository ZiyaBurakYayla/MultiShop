using FluentValidation;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;
        private readonly IValidator<CreateOrderingCommand> _validator;

        public CreateOrderingCommandHandler(IRepository<Ordering> repository, IValidator<CreateOrderingCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            await _repository.CreateAsync(new Ordering
            {
                UserId = request.UserId,
                TotalPrice = request.TotalPrice,
                OrderDate = request.OrderDate,
                IsPaid = request.IsPaid
            });
        }
    }
}
