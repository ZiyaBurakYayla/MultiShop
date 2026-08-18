using FluentValidation;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateSpecialOfferDtoValidator : AbstractValidator<CreateSpecialOfferDto>
    {
        public CreateSpecialOfferDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt başlık boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }

    public class UpdateSpecialOfferDtoValidator : AbstractValidator<UpdateSpecialOfferDto>
    {
        public UpdateSpecialOfferDtoValidator()
        {
            RuleFor(x => x.SpecialOfferId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt başlık boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }
}
