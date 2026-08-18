using FluentValidation;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;
        private readonly IValidator<UpdateOrderingCommand> _validator;

        public UpdateOrderingCommandHandler(IRepository<Ordering> repository, IValidator<UpdateOrderingCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var value = await _repository.GetByIdAsync(request.OrderingId);
            value.UserId = request.UserId;
            value.TotalPrice = request.TotalPrice;
            value.OrderDate = request.OrderDate;
            await _repository.UpdateAsync(value);
        }
    }
}
