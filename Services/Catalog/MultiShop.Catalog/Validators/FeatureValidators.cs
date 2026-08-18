using FluentValidation;
using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateFeatureDtoValidator : AbstractValidator<CreateFeatureDto>
    {
        public CreateFeatureDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("İkon boş olamaz.");
        }
    }

    public class UpdateFeatureDtoValidator : AbstractValidator<UpdateFeatureDto>
    {
        public UpdateFeatureDtoValidator()
        {
            RuleFor(x => x.FeatureId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("İkon boş olamaz.");
        }
    }
}
