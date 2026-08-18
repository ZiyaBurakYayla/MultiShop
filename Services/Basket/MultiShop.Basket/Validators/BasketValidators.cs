using FluentValidation;
using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Validators
{
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Adet 0'dan büyük olmalı.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı.");
        }
    }

    public class BasketTotalDtoValidator : AbstractValidator<BasketTotalDto>
    {
        public BasketTotalDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
            RuleFor(x => x.BasketItems).NotEmpty().WithMessage("Sepette en az bir ürün olmalı.");
            RuleForEach(x => x.BasketItems).SetValidator(new BasketItemDtoValidator());
        }
    }
}
