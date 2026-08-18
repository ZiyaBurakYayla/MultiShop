using FluentValidation;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateOfferDiscountDtoValidator : AbstractValidator<CreateOfferDiscountDto>
    {
        public CreateOfferDiscountDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt başlık boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
            RuleFor(x => x.ButtonTitle).NotEmpty().WithMessage("Buton başlığı boş olamaz.");
        }
    }

    public class UpdateOfferDiscountDtoValidator : AbstractValidator<UpdateOfferDiscountDto>
    {
        public UpdateOfferDiscountDtoValidator()
        {
            RuleFor(x => x.OfferDiscountId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt başlık boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
            RuleFor(x => x.ButtonTitle).NotEmpty().WithMessage("Buton başlığı boş olamaz.");
        }
    }
}
