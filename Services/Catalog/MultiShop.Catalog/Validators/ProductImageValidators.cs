using FluentValidation;
using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateProductImageDtoValidator : AbstractValidator<CreateProductImageDto>
    {
        public CreateProductImageDtoValidator()
        {
            RuleFor(x => x.Image1).NotEmpty().WithMessage("En az bir görsel giriniz.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
        }
    }

    public class UpdateProductImageDtoValidator : AbstractValidator<UpdateProductImageDto>
    {
        public UpdateProductImageDtoValidator()
        {
            RuleFor(x => x.ProductImagesId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Image1).NotEmpty().WithMessage("En az bir görsel giriniz.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
        }
    }
}
