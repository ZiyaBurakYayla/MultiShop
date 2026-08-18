using FluentValidation;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateFeatureSliderDtoValidator : AbstractValidator<CreateFeatureSliderDto>
    {
        public CreateFeatureSliderDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }

    public class UpdateFeatureSliderDtoValidator : AbstractValidator<UpdateFeatureSliderDto>
    {
        public UpdateFeatureSliderDtoValidator()
        {
            RuleFor(x => x.FeatureSliderId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }
}
