using FluentValidation;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;

namespace MultiShop.Order.Application.Features.Mediator.Validators.OrderingValidators
{
    public class UpdateOrderingCommandValidator : AbstractValidator<UpdateOrderingCommand>
    {
        public UpdateOrderingCommandValidator()
        {
            RuleFor(x => x.OrderingId).GreaterThan(0).WithMessage("Geçerli bir sipariş Id giriniz.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalı.");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Sipariş tarihi boş olamaz.");
        }
    }
}
