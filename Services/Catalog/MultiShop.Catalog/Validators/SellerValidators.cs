using FluentValidation;
using MultiShop.Catalog.Dtos.SellerDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateSellerDtoValidator : AbstractValidator<CreateSellerDto>
    {
        public CreateSellerDtoValidator()
        {
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Mağaza adı boş olamaz.")
                .MaximumLength(150).WithMessage("Mağaza adı en fazla 150 karakter olabilir.");
            RuleFor(x => x.OwnerUserId).NotEmpty().WithMessage("Satıcı kullanıcı bilgisi boş olamaz.");
        }
    }

    public class UpdateSellerDtoValidator : AbstractValidator<UpdateSellerDto>
    {
        public UpdateSellerDtoValidator()
        {
            RuleFor(x => x.SellerId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Mağaza adı boş olamaz.")
                .MaximumLength(150).WithMessage("Mağaza adı en fazla 150 karakter olabilir.");
            RuleFor(x => x.OwnerUserId).NotEmpty().WithMessage("Satıcı kullanıcı bilgisi boş olamaz.");
        }
    }
}
