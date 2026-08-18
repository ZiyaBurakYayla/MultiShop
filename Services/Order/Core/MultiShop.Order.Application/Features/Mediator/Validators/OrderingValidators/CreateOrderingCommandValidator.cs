using FluentValidation;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;

namespace MultiShop.Order.Application.Features.Mediator.Validators.OrderingValidators
{
    public class CreateOrderingCommandValidator : AbstractValidator<CreateOrderingCommand>
    {
        public CreateOrderingCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalı.");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Sipariş tarihi boş olamaz.");
        }
    }
}
