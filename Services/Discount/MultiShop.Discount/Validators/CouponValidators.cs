using FluentValidation;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Validators
{
    public class CreateCouponDtoValidator : AbstractValidator<CreateCouponDto>
    {
        public CreateCouponDtoValidator()
        {
            RuleFor(x => x.CouponCode).NotEmpty().WithMessage("Kupon kodu boş olamaz.")
                .MaximumLength(50).WithMessage("Kupon kodu en fazla 50 karakter olabilir.");
            RuleFor(x => x.CouponRate).InclusiveBetween(1, 100).WithMessage("İndirim oranı 1 ile 100 arasında olmalı.");
            RuleFor(x => x.ValidDate).NotEmpty().WithMessage("Geçerlilik tarihi boş olamaz.");
        }
    }

    public class UpdateCouponDtoValidator : AbstractValidator<UpdateCouponDto>
    {
        public UpdateCouponDtoValidator()
        {
            RuleFor(x => x.CouponId).GreaterThan(0).WithMessage("Geçerli bir kupon Id giriniz.");
            RuleFor(x => x.CouponCode).NotEmpty().WithMessage("Kupon kodu boş olamaz.")
                .MaximumLength(50).WithMessage("Kupon kodu en fazla 50 karakter olabilir.");
            RuleFor(x => x.CouponRate).InclusiveBetween(1, 100).WithMessage("İndirim oranı 1 ile 100 arasında olmalı.");
            RuleFor(x => x.ValidDate).NotEmpty().WithMessage("Geçerlilik tarihi boş olamaz.");
        }
    }
}
