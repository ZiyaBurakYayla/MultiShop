using FluentValidation;
using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }

    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel adresi boş olamaz.");
        }
    }
}
