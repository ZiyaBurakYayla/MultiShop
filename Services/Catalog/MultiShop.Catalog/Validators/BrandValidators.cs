using FluentValidation;
using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakter olabilir.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }

    public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandDtoValidator()
        {
            RuleFor(x => x.BrandId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakter olabilir.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }
}
