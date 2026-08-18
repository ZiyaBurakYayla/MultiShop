using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;

namespace MultiShop.Order.Application.Features.CQRS.Validators.OrderDetailValidators
{
    public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.");
            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalı.");
            RuleFor(x => x.ProductAmount).GreaterThan(0).WithMessage("Ürün adedi 0'dan büyük olmalı.");
            RuleFor(x => x.ProductTotalPrice).GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalı.");
            RuleFor(x => x.OrderingId).GreaterThan(0).WithMessage("Geçerli bir sipariş Id giriniz.");
        }
    }
}
